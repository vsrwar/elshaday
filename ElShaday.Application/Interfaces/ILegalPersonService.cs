using ElShaday.Application.DTOs.Requests;
using ElShaday.Application.DTOs.Responses;

namespace ElShaday.Application.Interfaces;

public interface ILegalPersonService : ICrudService<LegalPersonRequestDto, LegalPersonResponseDto>
{
    
}