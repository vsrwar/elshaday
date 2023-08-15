using ElShaday.Domain.Entities.Person.Abstractions;
using ElShaday.Domain.ValueObjects;

namespace ElShaday.Domain.Entities.Person;

public sealed class Employee : PhysicalPerson
{
    private IEnumerable<Department> _departments;
    public IReadOnlyCollection<Department> Departments => _departments.ToList();

    public Employee(string document, string name, string nickName, Address address)
        : base(document, name, nickName, address)
    {
    }

    public Employee(string document, string name, string nickName, Address address, IEnumerable<Department> departments)
        : base(document, name, nickName, address)
    {
        _departments = departments;
    }

    public void AddDepartament(Department department)
    {
        _departments = _departments.Append(department);
    }
}