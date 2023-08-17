using ElShaday.Application.DTOs.Requests;
using ElShaday.Application.DTOs.Responses;

namespace ElShaday.Application.Interfaces;

public interface IPhysicalPersonService : ICrudService<PhysicalPersonRequestDto, PhysicalPersonResponseDto>
{
    
}