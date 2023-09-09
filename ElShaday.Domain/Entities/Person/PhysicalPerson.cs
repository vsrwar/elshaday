using ElShaday.Domain.Configuration;
using ElShaday.Domain.ValueObjects.Documents;

namespace ElShaday.Domain.Entities.Person;

public sealed class PhysicalPerson : Abstractions.Person
{
    public Cpf Document { get; private set; }
    public string Name { get; private set; }
    public string? NickName { get; private set; }

    public PhysicalPerson(string document, string name, string nickName, Address address, PersonQualifier qualifier)
        : base(address, qualifier)
    {
        Document = new Cpf(document);
        if (!Document.Valid)
            throw new BusinessException("Invalid CPF.");

        Name = name;
        NickName = nickName;
        Type = PersonType.Physical;
    }
    
    // EF Constructor
    protected PhysicalPerson()
    {
        Type = PersonType.Physical;
    }

    public void SetAddressId(int addressId)
    {
        AddressId = addressId;
    }

    public override string GetResponsibleName()
    {
        return $"(physical) - {Name}";
    }
}