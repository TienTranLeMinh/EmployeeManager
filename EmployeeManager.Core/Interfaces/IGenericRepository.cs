using EmployeeManager.Core.Entities;
using System.Linq.Expressions;

namespace EmployeeManager.Core.Interfaces;
public interface IGenericRepository<TEntity, TId> where TEntity : BaseEntity<TId>
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    IEnumerable<TEntity> GetAll();
    Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);
    IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes);
    Task<TEntity> GetByIdAsync(TId id, params Expression<Func<TEntity, object>>[] includes);
    Task<TEntity> GetAsync(TId id);
    Task<TId> AddAsync(TEntity entity);
    Task<TId> UpdateAsync(TId TId, TEntity entity);
    Task<bool> DeleteAsync(TId TId);
    Task<Paginated<TEntity>> GetPagedAsync(int pageIndex,
                                            int pageSize,
                                            params Expression<Func<TEntity, object>>[] includes);
}

