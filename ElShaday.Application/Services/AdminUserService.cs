using AutoMapper;
using ElShaday.Application.DTOs.Requests;
using ElShaday.Application.DTOs.Responses;
using ElShaday.Application.Interfaces;
using ElShaday.Domain.Entities.User;
using ElShaday.Domain.Interfaces;
using ElShaday.Domain.ValueObjects;
using ApplicationException = ElShaday.Application.Configuration.ApplicationException;

namespace ElShaday.Application.Services;

public class AdminUserService : IAdminUserService
{
    private readonly IAdminUserRepository _repository;
    private readonly IMapper _mapper;

    public AdminUserService(IAdminUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<AdminUserResponseDto> CreateAsync(AdminUserRequestDto requestDto)
    {
        var entity = _mapper.Map<AdminUser>(requestDto);

        await ValidateNewAdminUserAsync(entity);
        
        await _repository.CreateAsync(entity);
        return _mapper.Map<AdminUserResponseDto>(entity);
    }

    public async Task<AdminUserResponseDto> UpdateAsync(AdminUserRequestDto requestDto)
    {
        var entity = _mapper.Map<AdminUser>(requestDto);

        await ValidateUpdateAdminUserAsync(entity);
        
        await _repository.UpdateAsync(entity);
        return _mapper.Map<AdminUserResponseDto>(entity);
    }

    public async Task<AdminUserResponseDto?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            return null;
        return _mapper.Map<AdminUserResponseDto>(entity);
    }

    public async Task<Paged<AdminUserResponseDto>> GetAsync(int page = 1, int pageSize = 25)
    {
        var pagedEntities = await _repository.GetAsync(page, pageSize);
        var dtos = _mapper.Map<IEnumerable<AdminUserResponseDto>>(pagedEntities.Entities);
        
        return new Paged<AdminUserResponseDto>(dtos, pagedEntities.Page, pagedEntities.PageSize, pagedEntities.Total, pagedEntities.TotalPages);
    }

    public async Task DeleteAsync(int id)
    {
        var exists = await IdExistsAsync(id);
        if (!exists)
            return;
        await _repository.DeleteAsync(id);
    }

    public async Task<bool> EmailExistsAsync(string email) => await _repository.EmailExistsAsync(email);

    public async Task<bool> NickNameExistsAsync(string nickName) => await _repository.NickNameExistsAsync(nickName);
    public async Task<bool> IdExistsAsync(int id) => await _repository.IdExistsAsync(id);
    public async Task DeactivateAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            throw new ApplicationException("Admin User not found");
        
        entity.Deactivate();
        await _repository.UpdateAsync(entity);
    }

    public async Task ActivateAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            throw new ApplicationException("Admin User not found");
        
        entity.Activete();
        await _repository.UpdateAsync(entity);
    }

    private async Task ValidateNewAdminUserAsync(AdminUser entity)
    {
        if (await EmailExistsAsync(entity.Email))
            throw new ApplicationException("Email already exists");

        if (await NickNameExistsAsync(entity.NickName))
            throw new ApplicationException("NickName already exists");
    }
    
    private async Task ValidateUpdateAdminUserAsync(AdminUser entity)
    {
        var savedEntity = await _repository.GetByIdAsync(entity.Id);
        if (savedEntity is null)
            throw new ApplicationException("Admin User not found");
        
        if(entity.Email != savedEntity.Email)
            throw new ApplicationException("Cannot change email");
        
        if(await NickNameExistsAsync(entity.NickName))
            throw new ApplicationException("NickName already exists");
    }
}