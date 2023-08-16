using AutoMapper;
using ElShaday.Application.DTOs.Requests;
using ElShaday.Application.DTOs.Responses;
using ElShaday.Application.Interfaces;
using ElShaday.Domain.Entities;
using ElShaday.Domain.Interfaces;
using ElShaday.Domain.ValueObjects;

namespace ElShaday.Application.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _repository;
    private readonly IMapper _mapper;

    public DepartmentService(IDepartmentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<DepartmentResponseDto> CreateAsync(DepartmentRequestDto requestDto)
    {
        await ValidateNewDepartmentAsync(requestDto);
        
        var entity = _mapper.Map<Department>(requestDto);
        await _repository.CreateAsync(entity);
        return _mapper.Map<DepartmentResponseDto>(entity);
    }
    
    public async Task<DepartmentResponseDto> UpdateAsync(DepartmentRequestDto requestDto)
    {
        var entity = _mapper.Map<Department>(requestDto);

        await ValidateUpdateDepartmentAsync(entity);
        
        await _repository.UpdateAsync(entity);
        return _mapper.Map<DepartmentResponseDto>(entity);
    }

    public async Task<DepartmentResponseDto?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            return null;
        return _mapper.Map<DepartmentResponseDto>(entity);
    }

    public async Task<Paged<DepartmentResponseDto>> GetAsync(int page = 1, int pageSize = 25)
    {
        var pagedEntities = await _repository.GetAsync(page, pageSize);
        var dtos = _mapper.Map<IEnumerable<DepartmentResponseDto>>(pagedEntities.Entities);
        
        return new Paged<DepartmentResponseDto>(dtos, pagedEntities.Page, pagedEntities.PageSize, pagedEntities.Total, pagedEntities.TotalPages);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task ChangeResponsibleAsync(int id, int responsibleId)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            return;

        await ValidateResponsableIdAsync(responsibleId);
        
        entity.UpdateResponsable(responsibleId);
        await _repository.UpdateAsync(entity);
    }

    private async Task ValidateResponsableIdAsync(int responsibleId)
    {
        throw new NotImplementedException();
    }

    private async Task ValidateNewDepartmentAsync(DepartmentRequestDto requestDto)
    {
        if (await _repository.NameExistsAsync(requestDto.Name))
            throw new ApplicationException("Department Name already exists");
        
        await ValidateResponsableIdAsync(requestDto.ResponsibleId);
    }

    private async Task ValidateUpdateDepartmentAsync(Department entity)
    {
        var savedDepartment = await _repository.GetByIdAsync(entity.Id);
        if (savedDepartment is null)
            throw new ApplicationException("Department not found");

        if (await _repository.NameExistsAsync(entity.Name))
            throw new ApplicationException("Department Name already exists");
        
        await ValidateResponsableIdAsync(entity.ResponsibleId);
    }
}