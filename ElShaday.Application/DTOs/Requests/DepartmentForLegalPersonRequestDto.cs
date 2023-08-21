using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ElShaday.Domain.Entities.Person;

namespace ElShaday.Application.DTOs.Requests;

public class DepartmentForLegalPersonRequestDto
{
    public int? Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int LegalPersonId { get; set; }
    [JsonIgnore] public PersonType Type { get; } = PersonType.Legal;
}