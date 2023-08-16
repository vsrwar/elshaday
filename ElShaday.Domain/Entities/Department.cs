using ElShaday.Domain.Entities.Person;

namespace ElShaday.Domain.Entities;

public sealed class Department : Entity
{
    public string Name { get; private set; }

    public int ResponsibleId { get; private set; }
    public Employee Responsible { get; private set; }

    public Department(string name, int responsibleId)
    {
        Name = name;
        ResponsibleId = responsibleId;
    }

    public void UpdateResponsable(int responsibleId)
    {
        ResponsibleId = responsibleId;
    }
}