using ElShaday.Domain.ValueObjects.Documents;

namespace ElShaday.Domain.Entities.Person.Abstractions;

public abstract class PhysicalPerson : Person
{
    public Cpf Document { get; private set; }
    public string Name { get; private set; }
    public string? NickName { get; private set; }

    public PhysicalPerson(string document, string name, string nickName, Address address) : base(address)
    {
        Document = new Cpf(document);
        Name = name;
        NickName = nickName;
    }
    
    // EF Constructor
    protected PhysicalPerson()
    { }
}