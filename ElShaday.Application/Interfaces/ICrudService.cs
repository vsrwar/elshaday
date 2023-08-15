using ElShaday.Domain.Entities;
using ElShaday.Domain.ValueObjects;

namespace ElShaday.Application.Interfaces;

public interface ICrudService<T> where T : Entity
{
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task<T?> GetByIdAsync(int id);
    Task<Paged<T>> GetAsync(int page = 1, int pageSize = 25);
    Task DeleteAsync(T entity);
}