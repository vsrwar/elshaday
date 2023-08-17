using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ElShaday.Domain.Entities.Person;

namespace ElShaday.Application.DTOs.Requests;

public class DepartmentForPhysicalPersonRequestDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public int PhysicalPersonId { get; set; }
    [JsonIgnore] public PersonType Type { get; } = PersonType.Physical;
}