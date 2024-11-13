using DrivingSchool.Application.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DrivingSchool.Persistence.Services;

internal abstract class Repository<TKey, TEntity, TContext>(TContext dbContext, DbSet<TEntity> entitySet) : IRepository<TKey, TEntity>
    where TEntity : class
    where TContext : DbContext
{
    protected TContext DbContext { get; } = dbContext;
    private readonly DbSet<TEntity> _entitySet = entitySet;

    public virtual async Task<TEntity?> CreateAsync(TEntity entity, bool persist = true)
    {
        this.DbContext.Add(entity);
        if (persist)
        {
            await this.DbContext.SaveChangesAsync();
        }
        return entity;
    }

    public virtual async Task<IList<TEntity>> CreateAsync(params TEntity[] entities)
    {
        foreach (var entity in entities)
        {
            _entitySet.Add(entity);
        }
        await this.DbContext.SaveChangesAsync();
        return entities;
    }

    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        => await _entitySet.AnyAsync(predicate);

    public virtual async Task<TEntity?> ReadAsync(TKey id)
        => await _entitySet.FindAsync(id);

    public virtual async Task<List<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> predicate)
        => await _entitySet.Where(predicate).ToListAsync();

    public IQueryable<TEntity> Read(Expression<Func<TEntity, bool>> predicate)
        => _entitySet.Where(predicate);

    public virtual async Task<TEntity?> UpdateAsync(TEntity? entity = null, bool persist = true)
    {
        if (entity != null)
        {
            this.DbContext.Update(entity);
        }
        if (persist)
        {
            await this.DbContext.SaveChangesAsync();
        }
        return entity;
    }

    public virtual async Task<IList<TEntity>> UpdateOrCreateAsync(params TEntity[] entities)
    {
        foreach (var entity in entities)
        {
            if (null != _entitySet.Local.FindEntry(entity))
            {
                await this.UpdateAsync(entity, false);
            }
            else
            {
                await this.CreateAsync(entity, false);
            }
        }
        await this.DbContext.SaveChangesAsync();
        return entities;
    }

    public virtual async Task<TEntity?> DeleteAsync(TKey id, bool persist = true)
    {
        var toDelete = await this.ReadAsync(id);
        if (toDelete == null)
        {
            return default;
        }
        this.DbContext.Remove(toDelete);
        if (persist)
        {
            await this.DbContext.SaveChangesAsync();
        }
        return toDelete;
    }
}