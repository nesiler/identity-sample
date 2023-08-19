using System.Linq.Expressions;
using ISP.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ISP.Application.Repositories.Base;

public interface IRepository<T> where T : class, IBaseEntity
{
    DbSet<T> DbSet { get; }

    IQueryable<T> GetAll(bool noTracking = true);

    IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, bool noTracking = true);

    Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate, bool noTracking = true);

    Task<T> GetByIdAsync(string id, bool noTracking = true);

    //get count
    Task<int> GetCountAsync();

    Task<bool> AddAsync(T entity);

    Task<bool> AddRangeAsync(ICollection<T> entities);

    bool Update(T entity);

    bool Delete(T entity);

    Task<bool> DeleteAsync(string id);

    bool DeleteRange(ICollection<T> entities);

    Task<int> SaveAsync();
}