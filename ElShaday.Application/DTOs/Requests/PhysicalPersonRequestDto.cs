using System.ComponentModel.DataAnnotations;
using ElShaday.Domain.Entities.Person;

namespace ElShaday.Application.DTOs.Requests;

public class PhysicalPersonRequestDto
{
    public int? Id { get; set; }
    [Required]
    public AddressRequestDto Address { get; set; }
    [Required]
    public PersonQualifier Qualifier { get; set; }
    [Required]
    [MinLength(11)]
    [MaxLength(11)]
    public string Cpf { get; set; }
    [Required]
    public string Name { get; set; }
    public string? NickName { get; set; }
}