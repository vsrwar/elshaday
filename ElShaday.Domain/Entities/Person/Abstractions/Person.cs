using System.ComponentModel.DataAnnotations.Schema;

namespace ElShaday.Domain.Entities.Person.Abstractions;

public abstract class Person : Entity
{
    public int AddressId { get; protected set; }
    public Address Address { get; private set; }
    public PersonQualifier Qualifier { get; private set; }
    public ICollection<Department.Department> Departments { get; set; }
    [NotMapped] public PersonType Type { get; protected set; }

    public Person(Address address, PersonQualifier qualifier)
    {
        Address = address;
        Qualifier = qualifier;
    }
    
    // EF Constructor
    protected Person()
    {
    }
}