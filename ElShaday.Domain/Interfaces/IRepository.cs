using System.Linq.Expressions;
using ElShaday.Domain.Entities;
using ElShaday.Domain.ValueObjects;

namespace ElShaday.Domain.Interfaces;

public interface IRepository<T> where T : Entity
{
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T?> GetByIdAsync(int id, params Expression<Func<T, Entity>>[] includes);
    Task<T?> GetByIdAsync(int id, params string[] includes);
    Task<T?> GetByIdAsync(int id);
    Task<Paged<T>> GetAsync(int page = 1, int pageSize = 25);
    Task DeleteAsync(int id);
    Task<int> CountActivesAsync();
}