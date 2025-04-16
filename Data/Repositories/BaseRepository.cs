using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Data.Repositories;

//TODO: Add a generic interface for the repository
//TODO: Add RepositoryResult to the CRUD. How to implement when uising Transaction Managment?
//TODO: Add a dynamic mapping extension to the Domain layer
public class BaseRepository<TEntity>(AppDbContext context) where TEntity : class
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

    public virtual async Task<IEnumerable<TEntity>> GetAllEntitiesAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeExpression = null) 
    {
        IQueryable<TEntity> query = _dbSet;
        if (includeExpression != null)
        {
            query = includeExpression(query);
        }
            var result = await query.ToListAsync();
            return result;
    }

    public virtual async Task<TEntity?> GetEntityAsync(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeExpression = null)
    {
        IQueryable<TEntity> query = _dbSet;
        if (includeExpression != null)
        {
            query = includeExpression(query);
        }
        var result = await query.FirstOrDefaultAsync(expression);
        return result;
    }

    // Update

    public virtual async Task UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity updatedEntity) 
    {
        var existingEntity = await _dbSet.FirstOrDefaultAsync(expression);
        if (existingEntity != null)
            _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
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
