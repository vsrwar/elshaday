using ElShaday.Domain.Entities.Department;
using ElShaday.Domain.Entities.Person;

namespace ElShaday.Domain.Interfaces;

public interface IDepartmentRepository : IRepository<Department>
{
    Task<bool> NameExistsAsync(string name);
    Task<bool> HasDepartmentsAsync(int personId, PersonType personType);
}