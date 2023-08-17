using AutoMapper;
using ElShaday.Application.DTOs.Requests;
using ElShaday.Application.DTOs.Responses;
using ElShaday.Application.Interfaces;
using ElShaday.Domain.Entities;
using ElShaday.Domain.Entities.Person;
using ElShaday.Domain.Interfaces;
using ElShaday.Domain.ValueObjects;

namespace ElShaday.Application.Services;

public class LegalPersonService : ILegalPersonService
{
    private readonly ILegalPersonRepository _repository;
    private readonly IRepository<Address> _addressRepository;
    private readonly IMapper _mapper;

    public LegalPersonService(ILegalPersonRepository repository, IMapper mapper, IRepository<Address> addressRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _addressRepository = addressRepository;
    }

    public async Task<LegalPersonResponseDto> CreateAsync(LegalPersonRequestDto requestDto)
    {
        await ValidateLegalPersonAsync(requestDto);
        
        var address = _mapper.Map<Address>(requestDto.Address);
        await _addressRepository.CreateAsync(address);
        
        var entity = _mapper.Map<LegalPerson>(requestDto);
        await _repository.CreateAsync(entity);
        return _mapper.Map<LegalPersonResponseDto>(entity);
    }

    public async Task<LegalPersonResponseDto> UpdateAsync(LegalPersonRequestDto requestDto)
    {
        await ValidateLegalPersonAsync(requestDto);
        
        var entity = _mapper.Map<LegalPerson>(requestDto);
        
        await _repository.UpdateAsync(entity);
        return _mapper.Map<LegalPersonResponseDto>(entity);
    }

    public async Task<LegalPersonResponseDto?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetFullByIdAsync(id);
        if (entity is null)
            return null;
        return _mapper.Map<LegalPersonResponseDto>(entity);
    }

    public async Task<Paged<LegalPersonResponseDto>> GetAsync(int page = 1, int pageSize = 25)
    {
        var pagedEntities = await _repository.GetAsync(page, pageSize);
        var dtos = _mapper.Map<IEnumerable<LegalPersonResponseDto>>(pagedEntities.Entities);
        
        return new Paged<LegalPersonResponseDto>(dtos, pagedEntities.Page, pagedEntities.PageSize, pagedEntities.Total, pagedEntities.TotalPages);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    private async Task ValidateLegalPersonAsync(LegalPersonRequestDto requestDto)
    {
        if (await _repository.DocumentExistsAsync(requestDto.Cnpj))
            throw new ApplicationException("Document already exists");
    }
}