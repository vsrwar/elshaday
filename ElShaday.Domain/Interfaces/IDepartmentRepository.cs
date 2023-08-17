using ElShaday.Domain.Entities.Department;

namespace ElShaday.Domain.Interfaces;

public interface IDepartmentRepository : IRepository<Department>
{
    Task<bool> NameExistsAsync(string name);
}