using ElShaday.Application.DTOs.Requests;
using ElShaday.Application.DTOs.Responses;
using ElShaday.Domain.Interfaces;

namespace ElShaday.Application.Interfaces;

public interface ILegalPersonService : ICrudService<LegalPersonRequestDto, LegalPersonResponseDto>, IEditable<LegalPersonRequestDto, LegalPersonResponseDto>
{
    Task<IEnumerable<LegalPersonResponseDto>> GetAvailableForDepartmentAsync();
    Task<int> CountActivesAsync();
}