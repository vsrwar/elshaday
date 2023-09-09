using AutoMapper;
using ElShaday.Application.DTOs.Requests;
using ElShaday.Application.DTOs.Responses;
using ElShaday.Application.Interfaces;
using ElShaday.Domain.Configuration;
using ElShaday.Domain.Entities.Department;
using ElShaday.Domain.Entities.Person;
using ElShaday.Domain.Entities.Person.Abstractions;
using ElShaday.Domain.Interfaces;
using ElShaday.Domain.ValueObjects;

namespace ElShaday.Application.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _repository;
    private readonly ILegalPersonRepository _legalPersonRepository;
    private readonly IPhysicalPersonRepository _physicalPersonRepository;
    private readonly IMapper _mapper;

    public DepartmentService(IDepartmentRepository repository, IMapper mapper,
        IPhysicalPersonRepository physicalPersonRepository, ILegalPersonRepository legalPersonRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _physicalPersonRepository = physicalPersonRepository;
        _legalPersonRepository = legalPersonRepository;
    }

    public async Task<DepartmentResponseDto> UpdateAsync(DepartmentForLegalPersonRequestDto forLegalPersonRequestDto)
    {
        var entity = _mapper.Map<Department>(forLegalPersonRequestDto);

        await ValidateUpdateDepartmentAsync(entity);
        
        await _repository.UpdateAsync(entity);
        return _mapper.Map<DepartmentResponseDto>(entity);
    }

    public async Task<DepartmentResponseDto?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            return null;

        int responsibleId = entity.LegalPersonId.HasValue ? entity.LegalPersonId.Value : entity.PhysicalPersonId!.Value;
        var responsible = await GetResponsible(responsibleId, entity.PersonType);
        if (responsible is null)
            return null;

        var departmentResponse = _mapper.Map<DepartmentResponseDto>(entity);
        departmentResponse.AddResponsible(responsible);

        return departmentResponse;
    }

    public async Task<Paged<DepartmentResponseDto>> GetAsync(int page = 1, int pageSize = 25)
    {
        var pagedEntities = await _repository.GetAsync(page, pageSize);
        var dtos = _mapper.Map<IEnumerable<DepartmentResponseDto>>(pagedEntities.Entities);
        
        foreach (var entity in pagedEntities.Entities)
        {
            int responsibleId = entity.LegalPersonId.HasValue ? entity.LegalPersonId.Value : entity.PhysicalPersonId!.Value;
            var responsible = await GetResponsible(responsibleId, entity.PersonType);
            if (responsible is null)
                continue;
            dtos.FirstOrDefault(x => x.Id == entity.Id)?.AddResponsible(responsible);
        }

        return new Paged<DepartmentResponseDto>(dtos, pagedEntities.Page, pagedEntities.PageSize, pagedEntities.Total, pagedEntities.TotalPages);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<DepartmentResponseDto> CreateAsync(DepartmentForPhysicalPersonRequestDto departmentForPhysicalPersonRequestDto)
    {
        var entity = _mapper.Map<Department>(departmentForPhysicalPersonRequestDto);
        await ValidateNewDepartmentAsync(entity);
        
        var user = await GetResponsible(departmentForPhysicalPersonRequestDto.PhysicalPersonId, PersonType.Physical);
        if (user is null)
            throw new BusinessException("Responsible not found");
        
        entity.UpdateResponsible(user);

        await _repository.CreateAsync(entity);
        return _mapper.Map<DepartmentResponseDto>(entity);
    }

    public async Task<DepartmentResponseDto> CreateAsync(DepartmentForLegalPersonRequestDto departmentForLegalPersonRequestDto)
    {
        var entity = _mapper.Map<Department>(departmentForLegalPersonRequestDto);
        await ValidateNewDepartmentAsync(entity);
        
        var user = await GetResponsible(departmentForLegalPersonRequestDto.LegalPersonId, PersonType.Legal);
        if (user is null)
            throw new BusinessException("Responsible not found");

        entity.UpdateResponsible(user);

        await _repository.CreateAsync(entity);
        return _mapper.Map<DepartmentResponseDto>(entity);
    }

    public async Task<int> CountActivesAsync()
        => await _repository.CountActivesAsync();

    public async Task<bool> HasDepartmentsAsync(int personId, PersonType personType)
    {
        return personType switch
        {
            PersonType.Legal => await _repository.HasDepartmentsAsync(personId, PersonType.Legal),
            PersonType.Physical => await _repository.HasDepartmentsAsync(personId, PersonType.Physical),
            _ => throw new BusinessException("Invalid Person Qualifier")
        };
    }

    private async Task ValidateNewDepartmentAsync(Department department)
    {
        if (await _repository.NameExistsAsync(department.Name))
            throw new BusinessException("Department Name already exists");
    }

    private async Task ValidateUpdateDepartmentAsync(Department entity)
    {
        var savedDepartment = await _repository.GetByIdAsync(entity.Id);
        if (savedDepartment is null)
            throw new BusinessException("Department not found");

        if (await _repository.NameExistsAsync(entity.Name))
            throw new BusinessException("Department Name already exists");
    }

    private async Task<Person?> GetResponsible(int responsibleId, PersonType type)
    {
        return type switch
        {
            PersonType.Legal => await _legalPersonRepository.GetByIdAsync(responsibleId),
            PersonType.Physical => await _physicalPersonRepository.GetByIdAsync(responsibleId),
            _ => throw new BusinessException("Invalid Person Qualifier")
        };
    }
}