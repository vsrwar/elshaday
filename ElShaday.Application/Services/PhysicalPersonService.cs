using AutoMapper;
using ElShaday.Application.DTOs.Requests;
using ElShaday.Application.DTOs.Responses;
using ElShaday.Application.Interfaces;
using ElShaday.Domain.Entities;
using ElShaday.Domain.Entities.Person;
using ElShaday.Domain.Interfaces;
using ElShaday.Domain.ValueObjects;
using ApplicationException = ElShaday.Application.Configuration.ApplicationException;

namespace ElShaday.Application.Services;

public class PhysicalPersonService : IPhysicalPersonService
{
    private readonly IPhysicalPersonRepository _repository;
    private readonly IMapper _mapper;

    public PhysicalPersonService(IPhysicalPersonRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PhysicalPersonResponseDto> CreateAsync(PhysicalPersonRequestDto requestDto)
    {
        try
        {
            await ValidatePhysicalPersonAsync(requestDto);
            var entity = _mapper.Map<PhysicalPerson>(requestDto);
            await _repository.CreateAsync(entity);

            await _repository.UpdateAsync(entity);
        
            return _mapper.Map<PhysicalPersonResponseDto>(entity);
        }
        catch (ApplicationException e)
        {
            throw e;
        }
    }

    public async Task<PhysicalPersonResponseDto> UpdateAsync(PhysicalPersonRequestDto requestDto)
    {
        await ValidatePhysicalPersonAsync(requestDto);
        var entity = _mapper.Map<PhysicalPerson>(requestDto);
        
        await _repository.UpdateAsync(entity);
        return _mapper.Map<PhysicalPersonResponseDto>(entity);
    }

    public async Task<IEnumerable<PhysicalPersonResponseDto>> GetAvailableForDepartmentAsync()
        => _mapper.Map<IEnumerable<PhysicalPersonResponseDto>>(await _repository.GetAvailableForDepartmentAsync());

    public async Task<PhysicalPersonResponseDto?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetFullByIdAsync(id);
        if (entity is null)
            return null;
        return _mapper.Map<PhysicalPersonResponseDto>(entity);
    }

    public async Task<Paged<PhysicalPersonResponseDto>> GetAsync(int page = 1, int pageSize = 25)
    {
        var pagedEntities = await _repository.GetAsync(page, pageSize);
        var dtos = _mapper.Map<IEnumerable<PhysicalPersonResponseDto>>(pagedEntities.Entities);
        
        return new Paged<PhysicalPersonResponseDto>(dtos, pagedEntities.Page, pagedEntities.PageSize, pagedEntities.Total, pagedEntities.TotalPages);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<int> CountActivesAsync()
        => await _repository.CountActivesAsync();

    private async Task ValidatePhysicalPersonAsync(PhysicalPersonRequestDto requestDto)
    {
        if (await _repository.DocumentExistsAsync(requestDto.Id, requestDto.Cpf))
            throw new System.ApplicationException("Document already exists");
    }
}