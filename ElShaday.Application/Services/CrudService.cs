using ElShaday.Application.Interfaces;
using ElShaday.Domain.Entities;
using ElShaday.Domain.Interfaces;
using ElShaday.Domain.ValueObjects;

namespace ElShaday.Application.Services;

public class CrudService<T> : ICrudService<T> where T : Entity 
{
    private readonly IRepository<T> _repository;

    public CrudService(IRepository<T> repository)
    {
        _repository = repository;
    }

    public async Task CreateAsync(T entity) => await _repository.CreateAsync(entity);

    public async Task UpdateAsync(T entity) => await _repository.UpdateAsync(entity);

    public async Task<T?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

    public async Task<Paged<T>> GetAsync(int page = 1, int pageSize = 25) => await _repository.GetAsync(page, pageSize);

    public async Task DeleteAsync(T entity) => await _repository.DeleteAsync(entity);
}