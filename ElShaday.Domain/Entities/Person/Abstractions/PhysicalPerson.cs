using ElShaday.Domain.Factories;
using ElShaday.Domain.ValueObjects;

namespace ElShaday.Domain.Entities.Person.Abstractions;

public abstract class PhysicalPerson : Person
{
    public Document Document { get; private set; }
    public string Name { get; private set; }
    public string NickName { get; private set; }

    public PhysicalPerson(string document, string name, string nickName, Address address) : base(address)
    {
        if(document.Length != DocumentFactory.CPF_LENGTH)
            throw new ArgumentException($"The document must have {DocumentFactory.CPF_LENGTH} characters.", nameof(document));

        Document = DocumentFactory.Create(document);
        Name = name;
        NickName = nickName;
    }
}