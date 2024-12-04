using EmployeeManager.Core.Entities;
using EmployeeManager.Core.Interfaces;
using EmployeeManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeManager.Infrastructure.Repositories;
public class GenericRepository<TEntity, TId> : IGenericRepository<TEntity, TId> where TEntity : BaseEntity<TId>
{
    private readonly ApplicationDbContext _context;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TId> AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> DeleteAsync(TId id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        if (entity != null && !entity.IsDeleted)
        {
            //_context.Set<TEntity>().Remove(entity);
            entity.IsDeleted = true;
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public IEnumerable<TEntity> GetAll() => _context.Set<TEntity>().Where(x => !x.IsDeleted).AsNoTracking().ToList();

    public IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
        => _context.Set<TEntity>().AsNoTracking().ToList();

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    => await _context.Set<TEntity>().Where(x => !x.IsDeleted).AsNoTracking().ToListAsync();

    public async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _context.Set<TEntity>().Where(x => !x.IsDeleted).AsQueryable();
        foreach (var item in includes)
        {
            query = query.Include(item);
        }
        return await query.ToListAsync();
    }

    public async Task<TEntity> GetAsync(TId id)
    {
        var entity_value = await _context.Set<TEntity>().FindAsync(id);
        if (entity_value == null || entity_value.IsDeleted)
        {
            return null;
        }
        return entity_value;
    }

    public async Task<TEntity> GetByIdAsync(TId id, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>().Where(x => x.Id.Equals(id) && !x.IsDeleted);
        foreach (var item in includes)
        {
            query = query.Include(item);
        }
        return await query.FirstOrDefaultAsync();
    }

    public async Task<Paginated<TEntity>> GetPagedAsync(int pageIndex, int pageSize, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>().Where(x => !x.IsDeleted);

        foreach (var item in includes)
        {
            query = query.Include(item);
        }

        var totalCount = await query.CountAsync();

        var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

        return new Paginated<TEntity>
        {
            Entities = items,
            TotalCount = (int)Math.Ceiling((double)totalCount / pageSize),
            PageIndex = pageIndex,
            PageSize = pageSize,
        };

    }

    public async Task<TId> UpdateAsync(TId id, TEntity entity)
    {
        var entity_value = await _context.Set<TEntity>().FindAsync(id);
        if (entity_value != null && !entity_value.IsDeleted)
        {
            _context.Update(entity_value);
            await _context.SaveChangesAsync();
        }

        return entity_value.Id;
    }
}
