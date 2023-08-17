using ElShaday.Domain.Entities.Person;
using ElShaday.Domain.ValueObjects.Documents;

namespace ElShaday.Application.DTOs.Responses;

public class PhysicalPersonResponseDto
{
    public int Id { get; set; }
    public int AddressId { get; set; }
    public AddressResponseDto Address { get; set; }
    public PersonQualifier Qualifier { get; set; }
    public Cpf Cpf { get; set; }
    public string Name { get; set; }
    public string? NickName { get; set; }
    public PersonType Type { get; set; }
    public ICollection<DepartmentResponseDto> Departments { get; set; }
}