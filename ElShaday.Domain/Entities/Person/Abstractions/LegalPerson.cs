using ElShaday.Domain.Factories;
using ElShaday.Domain.ValueObjects;

namespace ElShaday.Domain.Entities.Person.Abstractions;

public abstract class LegalPerson : Person
{
    public Document Document { get; private set; }
    public string CorporateName { get; private set; }
    public string FantasyName { get; private set; }

    public LegalPerson(string document, string corporateName, string fantasyName, Address address) : base(address)
    {
        if(document.Length != DocumentFactory.CNPJ_LENGTH)
            throw new ArgumentException($"The document must have {DocumentFactory.CNPJ_LENGTH} characters.", nameof(document));

        Document = DocumentFactory.Create(document);
        CorporateName = corporateName;
        FantasyName = fantasyName;
    }
}