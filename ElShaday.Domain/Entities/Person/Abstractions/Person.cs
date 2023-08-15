using ElShaday.Domain.ValueObjects;

namespace ElShaday.Domain.Entities.Person.Abstractions;

public abstract class Person : Entity
{
    public int AddressId { get; private set; }
    public Address Address { get; private set; }

    public Person(Address address)
    {
        Address = address;
    }
    
    // EF Constructor
    protected Person(){}
}