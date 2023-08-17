using ElShaday.Application.DTOs.Requests;
using ElShaday.Application.DTOs.Responses;
using ElShaday.Domain.ValueObjects;

namespace ElShaday.Application.Interfaces;

public interface IDepartmentService
{
    Task<DepartmentResponseDto> UpdateAsync(DepartmentForLegalPersonRequestDto dto);
    Task<DepartmentResponseDto?> GetByIdAsync(int id);
    Task<Paged<DepartmentResponseDto>> GetAsync(int page = 1, int pageSize = 25);
    Task DeleteAsync(int id);
    Task<DepartmentResponseDto> CreateAsync(DepartmentForPhysicalPersonRequestDto departmentForPhysicalPersonRequestDto);
    Task<DepartmentResponseDto> CreateAsync(DepartmentForLegalPersonRequestDto departmentForLegalPersonRequestDto);
}