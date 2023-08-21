using System.ComponentModel.DataAnnotations;
using ElShaday.Domain.Entities.Person;

namespace ElShaday.Application.DTOs.Requests;

public class LegalPersonRequestDto
{
    public int? Id { get; set; }
    [Required]
    public AddressRequestDto Address { get; set; }
    [Required]
    public PersonQualifier Qualifier { get; set; }
    [Required]
    [MinLength(14)]
    [MaxLength(14)]
    public string Cnpj { get; set; }
    [Required]
    [MinLength(3)]
    [MaxLength(255)]
    public string CorporateName { get; set; }
    [MaxLength(255)]
    public string FantasyName { get; set; }
}