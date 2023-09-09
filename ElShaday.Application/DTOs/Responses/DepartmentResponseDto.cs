using ElShaday.Domain.Entities.Person.Abstractions;

namespace ElShaday.Application.DTOs.Responses;

public class DepartmentResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ResponsableId { get; set; }
    public string ResponsableName { get; set; }

    public void AddResponsible(Person responsible)
    {
        ResponsableId = responsible.Id;
        ResponsableName = responsible.GetResponsibleName();
    }
}