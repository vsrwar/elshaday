using ElShaday.Domain.Entities.Person.Abstractions;

namespace ElShaday.Domain.Entities.Person;

public class CustomerLegalPerson : LegalPerson
{
    public CustomerLegalPerson(string document, string corporateName, string fantasyName, Address address)
        : base(document, corporateName, fantasyName, address)
    {
    }
    
    // EF Constructor
    private CustomerLegalPerson()
    { }
}