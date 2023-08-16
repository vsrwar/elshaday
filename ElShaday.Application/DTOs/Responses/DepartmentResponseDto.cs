using ElShaday.Domain.Entities.Person;

namespace ElShaday.Application.DTOs.Responses;

public class DepartmentResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ResponsibleId { get; set; }
    public Employee Responsible { get; set; }
}