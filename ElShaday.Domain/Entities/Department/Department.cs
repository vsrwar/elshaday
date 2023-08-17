using ElShaday.Domain.Entities.Person;

namespace ElShaday.Domain.Entities.Department;

public class Department : Entity
{
    public string Name { get; private set; }
    public int? LegalPersonId { get; private set; }
    public int? PhysicalPersonId { get; private set; }
    public PersonType PersonType { get; protected set; }

    public Department(string name, PersonType personType)
    {
        Name = name;
        PersonType = personType;
    }

    // EF Constructor
    private Department()
    {
    }

    public void UpdateResponsible(Person.Abstractions.Person user)
    {
        ValidateUser(user);
        switch (user.Type)
        {
            case PersonType.Legal:
                LegalPersonId = user.Id;
                break;
            case PersonType.Physical:
                PhysicalPersonId = user.Id;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ValidateUser(Person.Abstractions.Person user)
    {
        if (user.Qualifier != PersonQualifier.Employee)
            throw new ApplicationException("User must be an Employee");
    }
}