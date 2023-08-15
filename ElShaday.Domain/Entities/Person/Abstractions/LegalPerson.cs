using ElShaday.Domain.ValueObjects.Documents;

namespace ElShaday.Domain.Entities.Person.Abstractions;

public abstract class LegalPerson : Person
{
    public Cnpj Document { get; private set; }
    public string CorporateName { get; private set; }
    public string FantasyName { get; private set; }

    public LegalPerson(string document, string corporateName, string fantasyName, Address address) : base(address)
    {
        Document = new Cnpj(document);
        CorporateName = corporateName;
        FantasyName = fantasyName;
    }
    
    // EF Constructor
    protected LegalPerson()
    { }
}