namespace ElShaday.Domain.Interfaces;

public interface IEditable<TRequest, TResponse>
{
    Task<TResponse> UpdateAsync(TRequest dto);
}