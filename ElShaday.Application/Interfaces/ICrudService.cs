using ElShaday.Domain.ValueObjects;

namespace ElShaday.Application.Interfaces;

public interface ICrudService<TRequest, TResponse>
{
    Task<TResponse> CreateAsync(TRequest dto);
    Task<TResponse?> GetByIdAsync(int id);
    Task<Paged<TResponse>> GetAsync(int page = 1, int pageSize = 25);
    Task DeleteAsync(int id);
}