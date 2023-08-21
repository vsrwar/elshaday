using ElShaday.Application.DTOs.Requests;
using ElShaday.Application.DTOs.Responses;
using ElShaday.Domain.Interfaces;

namespace ElShaday.Application.Interfaces;

public interface IPhysicalPersonService : ICrudService<PhysicalPersonRequestDto, PhysicalPersonResponseDto>, IEditable<PhysicalPersonRequestDto, PhysicalPersonResponseDto>
{
    Task<IEnumerable<PhysicalPersonResponseDto>> GetAvailableForDepartmentAsync();
    Task<int> CountActivesAsync();
}