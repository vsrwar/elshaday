using ElShaday.Domain.Entities.Person;
using ElShaday.Domain.ValueObjects.Documents;

namespace ElShaday.Application.DTOs.Responses;

public class LegalPersonResponseDto
{
    public int Id { get; set; }
    public int AddressId { get; set; }
    public AddressResponseDto Address { get; set; }
    public PersonQualifier Qualifier { get; set; }
    public Cnpj Cnpj { get; set; }
    public string CorporateName { get; set; }
    public string FantasyName { get; set; }
    public PersonType Type { get; set; }
    public ICollection<DepartmentResponseDto> Departments { get; set; }
}