using ElShaday.Application.DTOs.Requests;
using ElShaday.Application.DTOs.Responses;

namespace ElShaday.Application.Interfaces;

public interface IDepartmentService : ICrudService<DepartmentRequestDto, DepartmentResponseDto>
{
    Task ChangeResponsibleAsync(int id, int responsibleId);
}