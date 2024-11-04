using System.Linq.Expressions;

namespace DrivingSchool.Application.Data;

public interface IRepository<TKey, TEntity>
{
    Task<TEntity?> CreateAsync(TEntity entity, bool persist = true);
    Task<IList<TEntity>> CreateAsync(params TEntity[] entities);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> ReadAsync(TKey id);
    Task<List<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> predicate);
    IQueryable<TEntity> Read(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> UpdateAsync(TEntity? entity = default, bool persist = true);
    Task<IList<TEntity>> UpdateOrCreateAsync(params TEntity[] entities);
    Task<TEntity?> DeleteAsync(TKey id, bool persist = true);
}