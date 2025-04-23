using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;
using Shared.Extensions;
using Shared.Results;

namespace Data.Repositories;

// TODO: Add a generic interface for the repository
// TODO: Add sorting, filtering and including to the GetAllAsync method
// The methods are virtual so that i can implement eager loading
public class BaseRepository<TEntity, TModel>(AppDbContext context) where TEntity : class where TModel : class
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
    public virtual async Task<Result> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeExpression = null) 
    {
        IQueryable<TEntity> query = _dbSet;
        if (includeExpression != null)
        {
            query = includeExpression(query);
        }
            var result = await query.ToListAsync();

        if (result.Count == 0)
            return Result.NotFound("No entities found");

        var mappedResult = result.Select(x => x.MapTo<TModel>()).ToList();
        return RepositoryResult<IEnumerable<TModel>>.Ok(mappedResult);
    }

    public virtual async Task<Result> GetAsync(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeExpression = null)
    {
        IQueryable<TEntity> query = _dbSet;
        if (includeExpression != null)
        {
            query = includeExpression(query);
        }
        var result = await query.FirstOrDefaultAsync(expression);
        if (result == null)
            return Result.NotFound("Entity not found");

        var mappedResult = result.MapTo<TModel>();
        return RepositoryResult<TModel>.Ok(mappedResult);
    }


    // Update
    public virtual async Task<Result> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity updatedEntity) 
    {
        var existingEntity = await _dbSet.FirstOrDefaultAsync(expression);
        if (existingEntity == null)
        { 
           return Result.NotFound("Entity not found");
        }  
        _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
        return Result.Ok();
    }

    // Delete
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