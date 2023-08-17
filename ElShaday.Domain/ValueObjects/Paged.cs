namespace ElShaday.Domain.ValueObjects;

public class Paged<T>
{
    public int Page { get; private set; }
    public int PageSize { get; private set; }
    public int Total { get; private set; }
    public int TotalPages { get; private set; }
    public IReadOnlyCollection<T> Entities => _entities.ToList();

    private readonly IEnumerable<T> _entities;

    public Paged(IEnumerable<T> entities, int page, int pageSize, int total, int totalPages)
    {
        Page = page;
        PageSize = pageSize;
        Total = total;
        TotalPages = totalPages;
        _entities = entities;
    }
}