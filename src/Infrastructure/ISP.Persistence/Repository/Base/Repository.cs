using System.Linq.Expressions;
using ISP.Application.Repositories.Base;
using ISP.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ISP.Persistence.Repository.Base;

public class Repository<T, TContext> : IRepository<T> where T : class, IBaseEntity
    where TContext : DbContext
{
    private readonly TContext _context;

    protected Repository(TContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public DbSet<T> DbSet => _context.Set<T>();

    public IQueryable<T> GetAll(bool noTracking = true) =>
        noTracking ? DbSet.AsNoTracking() : DbSet.AsQueryable();

    public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, bool noTracking = true) => noTracking
        ? DbSet.Where(predicate).AsNoTracking()
        : DbSet.Where(predicate);

    public async Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate, bool noTracking = true) =>
        await (noTracking ? DbSet.AsNoTracking() : DbSet).FirstOrDefaultAsync(predicate);

    public async Task<T> GetByIdAsync(string id, bool noTracking = true) =>
        await (noTracking ? DbSet.AsNoTracking() : DbSet).FirstOrDefaultAsync(x => x.Id == id);

    public async Task<int> GetCountAsync() => await DbSet.CountAsync();

    public async Task<bool> AddAsync(T entity)
    {
        var entityEntry = await DbSet.AddAsync(entity);
        return entityEntry.State == EntityState.Added;
    }

    public async Task<bool> AddRangeAsync(ICollection<T> entities)
    {
        await DbSet.AddRangeAsync(entities);
        return true;
    }

    public bool Update(T entity)
    {
        var entityEntry = DbSet.Update(entity);
        return entityEntry.State == EntityState.Modified;
    }

    public bool Delete(T entity)
    {
        var entityEntry = DbSet.Remove(entity);
        return entityEntry.State == EntityState.Deleted;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var entity = await DbSet.FirstOrDefaultAsync(d => d.Id == id);
        return Delete(entity);
    }

    public bool DeleteRange(ICollection<T> entities)
    {
        DbSet.RemoveRange(entities);
        return true;
    }

    public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
}