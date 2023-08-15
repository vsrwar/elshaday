using ElShaday.Domain.Entities.Person.Abstractions;

namespace ElShaday.Domain.Entities.Person;

public class CustomerPhysicalPerson : PhysicalPerson
{
    public CustomerPhysicalPerson(string document, string name, string nickName, Address address)
        : base(document, name, nickName, address)
    {
    }

    // EF Constructor
    private CustomerPhysicalPerson()
    { }
}