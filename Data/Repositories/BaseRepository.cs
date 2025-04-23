using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;
using Shared.Extensions;
using Shared.Results;

namespace Data.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task AddAsync(TEntity entity);
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    void Delete(TEntity entity);
    Task<bool> EntityExistsAsync(Expression<Func<TEntity, bool>> expression);
    Task<Result> GetAllAsync(bool orderByDescending = false, Expression<Func<TEntity, object>>? sortBy = null, Expression<Func<TEntity, bool>>? where = null, params Expression<Func<TEntity, object>>[] includes);
    Task<Result> GetAllAsync<TSelect>(Expression<Func<TEntity, TSelect>> selector, bool orderByDescending = false, Expression<Func<TEntity, object>>? sortBy = null, Expression<Func<TEntity, bool>>? where = null, params Expression<Func<TEntity, object>>[] includes);
    Task<Result> GetAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes);
    Task RollbackTransactionAsync();
    Task<int> SaveAsync();
    Task<Result> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity updatedEntity);
}

// The methods are virtual so that i can implement eager loading
public class BaseRepository<TEntity, TModel>(AppDbContext context) : IBaseRepository<TEntity> where TEntity : class where TModel : class
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
    public virtual async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    // Read

    public virtual async Task<Result> GetAllAsync
        (bool orderByDescending = false, Expression<Func<TEntity, object>>? sortBy = null, Expression<Func<TEntity, bool>>? where = null, params Expression<Func<TEntity, object>>[] includes)
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
            return Result.NotFound("No entities found");
        }
        var result = entities.Select(entitiy => entitiy.MapTo<TModel>()).ToList();
        return RepositoryResult<IEnumerable<TModel>>.Ok(result);
    }


    public virtual async Task<Result> GetAllAsync<TSelect>
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
            return Result.NotFound("No entities found.");
        }
        //var result = entities.Select(entitiy => entitiy!.MapTo<TSelect>()).ToList();
        return RepositoryResult<IEnumerable<TSelect>>.Ok(entities);
    }






    public virtual async Task<Result> GetAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes)
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
            return Result.NotFound("Entity not found.");

        var result = entity.MapTo<TModel>();
        return RepositoryResult<TModel>.Ok(result);
    }


    // Update
    public virtual async Task<Result> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity updatedEntity)
    {
        var existingEntity = await _dbSet.FirstOrDefaultAsync(expression);
        if (existingEntity == null)
        {
            return Result.NotFound("Entity not found.");
        }
        _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
        return Result.Ok();
    }

    // Delete
    // TODO: Delete by id
    public virtual void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
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