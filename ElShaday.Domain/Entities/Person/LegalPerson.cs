using ElShaday.Domain.Configuration;
using ElShaday.Domain.ValueObjects.Documents;

namespace ElShaday.Domain.Entities.Person;

public sealed class LegalPerson : Abstractions.Person
{
    public Cnpj Document { get; private set; }
    public string CorporateName { get; private set; }
    public string FantasyName { get; private set; }

    public LegalPerson(string document, string corporateName, string fantasyName, Address address, PersonQualifier qualifier)
        : base(address, qualifier)
    {
        Document = new Cnpj(document);
        if (!Document.Valid)
            throw new BusinessException("Invalid CNPJ.");
        
        CorporateName = corporateName;
        FantasyName = fantasyName;
        Type = PersonType.Legal;
    }
    
    // EF Constructor
    private LegalPerson()
    {
        Type = PersonType.Legal;
    }

    public override string GetResponsibleName()
    {
        return $"(legal) - {CorporateName}";
    }
}