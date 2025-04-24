using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;
using Shared.Extensions;
using Shared.Results;

namespace Data.Repositories;

public interface IBaseRepository<TEntity, TModel> where TEntity : class where TModel : class
{
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    Task<Result<TModel>> AddAsync(TEntity entity);
    Task<Result<IEnumerable<TModel>>> GetAllAsync(bool orderByDescending = false, Expression<Func<TEntity, object>>? sortBy = null, Expression<Func<TEntity, bool>>? where = null, params Expression<Func<TEntity, object>>[] includes);
    Task<Result<IEnumerable<TSelect>>> GetAllAsync<TSelect>(Expression<Func<TEntity, TSelect>> selector, bool orderByDescending = false, Expression<Func<TEntity, object>>? sortBy = null, Expression<Func<TEntity, bool>>? where = null, params Expression<Func<TEntity, object>>[] includes);
    Task<Result<TModel>> GetAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes);
    Task<Result<TModel>> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity updatedEntity);
    Result<TModel> Delete(TModel model);
    Task<int> SaveAsync();
    Task<bool> EntityExistsAsync(Expression<Func<TEntity, bool>> expression);
}

// The methods are virtual so that i can implement eager loading
public class BaseRepository<TEntity, TModel>(AppDbContext context) : IBaseRepository<TEntity, TModel> where TEntity : class where TModel : class
{
    protected readonly AppDbContext _context = context;

    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    private IDbContextTransaction _transaction = null!;

    #region Transaction Managment
    public virtual async Task BeginTransactionAsync()
    {
        _transaction ??= await _context.Database.BeginTransactionAsync();
    }
    public virtual async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            //reset transaction
            _transaction = null!;
        }
    }

    public virtual async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            //reset
            _transaction = null!;
        }
    }
    #endregion Transaction Managment

    #region CRUD Operations
    // Create
    public virtual async Task<Result<TModel>> AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        return Result<TModel>.Created();
    }

    // Read

    public virtual async Task<Result<IEnumerable<TModel>>> GetAllAsync
        (
        bool orderByDescending = false, 
        Expression<Func<TEntity, object>>? sortBy = null, 
        Expression<Func<TEntity, bool>>? where = null, 
        params Expression<Func<TEntity, object>>[] includes
        )
    {

        IQueryable<TEntity> query = _dbSet;
        if (where != null)
        {
            query = query.Where(where);
        }

        if (includes != null && includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        if (sortBy != null)
        {
            query = orderByDescending
                ? query.OrderByDescending(sortBy)
                : query.OrderBy(sortBy);
        }

        var entities = await query.ToListAsync();
        if (entities.Count == 0)
        {
            return Result<IEnumerable<TModel>>.NotFound("No entities found");
        }
        var result = entities.Select(entitiy => entitiy.MapTo<TModel>()).ToList();
        return Result<IEnumerable<TModel>>.Ok(result);
    }


    public virtual async Task<Result<IEnumerable<TSelect>>> GetAllAsync<TSelect>
        (
        Expression<Func<TEntity, TSelect>> selector,
        bool orderByDescending = false,
        Expression<Func<TEntity, object>>? sortBy = null,
        Expression<Func<TEntity, bool>>? where = null,
        params Expression<Func<TEntity, object>>[] includes
        )
    {

        IQueryable<TEntity> query = _dbSet;
        if (where != null)
        {
            query = query.Where(where);
        }

        if (includes != null && includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        if (sortBy != null)
        {
            query = orderByDescending
                ? query.OrderByDescending(sortBy)
                : query.OrderBy(sortBy);
        }

        var entities = await query.Select(selector).ToListAsync();
        if (entities.Count == 0)
        {
            return Result<IEnumerable<TSelect>>.NotFound("No entities found.");
        }
        //var result = entities.Select(entitiy => entitiy!.MapTo<TSelect>()).ToList();
        return Result<IEnumerable<TSelect>>.Ok(entities);
    }

    public virtual async Task<Result<TModel>> GetAsync
        (
        Expression<Func<TEntity, bool>> where, 
        params Expression<Func<TEntity, object>>[] includes
        )
    {
        IQueryable<TEntity> query = _dbSet;

        if (includes != null && includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        var entity = await query.FirstOrDefaultAsync(where);

        if (entity == null)
            return Result<TModel>.NotFound("Entity not found.");

        var result = entity.MapTo<TModel>();
        return Result<TModel>.Ok(result);
    }


    // Update
    public virtual async Task<Result<TModel>> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity updatedEntity)
    {
        var existingEntity = await _dbSet.FirstOrDefaultAsync(expression);
        if (existingEntity == null)
        {
            return Result<TModel>.NotFound("Entity not found.");
        }
        _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
        return Result<TModel>.Updated();
    }

    // Delete
    // TODO: Delete by id
    public virtual Result<TModel> Delete(TModel model)
    {
        if (model == null)
        { 
            return Result<TModel>.BadRequest("Can't delete a null entity"); 
        }
        _dbSet.Remove(model.MapTo<TEntity>());
        return Result<TModel>.Deleted();
    }

    // Save Changes
    public virtual async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
    #endregion CRUD Operations

    public virtual async Task<bool> EntityExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _dbSet.AnyAsync(expression);
    }
}