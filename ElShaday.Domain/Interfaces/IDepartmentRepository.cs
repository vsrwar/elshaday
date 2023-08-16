using ElShaday.Domain.Entities;

namespace ElShaday.Domain.Interfaces;

public interface IDepartmentRepository : IRepository<Department>
{
    Task<bool> NameExistsAsync(string name);
}