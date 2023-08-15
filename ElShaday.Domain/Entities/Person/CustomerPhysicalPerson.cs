using ElShaday.Domain.Entities.Person.Abstractions;
using ElShaday.Domain.ValueObjects;

namespace ElShaday.Domain.Entities.Person;

public class CustomerPhysicalPerson : PhysicalPerson
{
    public CustomerPhysicalPerson(string document, string name, string nickName, Address address)
        : base(document, name, nickName, address)
    {
    }
}