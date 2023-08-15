using ElShaday.Domain.Entities.Person.Abstractions;
using ElShaday.Domain.ValueObjects;

namespace ElShaday.Domain.Entities.Person;

public class CustomerLegalPerson : LegalPerson
{
    public CustomerLegalPerson(string document, string corporateName, string fantasyName, Address address)
        : base(document, corporateName, fantasyName, address)
    {
    }
}