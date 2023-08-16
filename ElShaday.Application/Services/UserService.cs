using AutoMapper;
using ElShaday.Application.DTOs.Requests;
using ElShaday.Application.DTOs.Responses;
using ElShaday.Application.Interfaces;
using ElShaday.Domain.Entities.User;
using ElShaday.Domain.Interfaces;
using ElShaday.Domain.ValueObjects;
using ApplicationException = ElShaday.Application.Configuration.ApplicationException;

namespace ElShaday.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UserResponseDto> CreateAsync(UserRequestDto requestDto)
    {
        await ValidateNewUserAsync(requestDto);
        
        var entity = _mapper.Map<User>(requestDto);
        await _repository.CreateAsync(entity);
        return _mapper.Map<UserResponseDto>(entity);
    }

    public async Task<UserResponseDto> UpdateAsync(UserRequestDto requestDto)
    {
        var entity = _mapper.Map<User>(requestDto);

        await ValidateUpdateUserAsync(entity);
        
        await _repository.UpdateAsync(entity);
        return _mapper.Map<UserResponseDto>(entity);
    }

    public async Task<UserResponseDto?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            return null;
        return _mapper.Map<UserResponseDto>(entity);
    }

    public async Task<Paged<UserResponseDto>> GetAsync(int page = 1, int pageSize = 25)
    {
        var pagedEntities = await _repository.GetAsync(page, pageSize);
        var dtos = _mapper.Map<IEnumerable<UserResponseDto>>(pagedEntities.Entities);
        
        return new Paged<UserResponseDto>(dtos, pagedEntities.Page, pagedEntities.PageSize, pagedEntities.Total, pagedEntities.TotalPages);
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
            throw new ApplicationException("User not found");
        
        entity.Deactivate();
        await _repository.UpdateAsync(entity);
    }

    public async Task ActivateAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            throw new ApplicationException("User not found");
        
        entity.Activete();
        await _repository.UpdateAsync(entity);
    }

    private async Task ValidateNewUserAsync(UserRequestDto dto)
    {
        if (!Enum.IsDefined(typeof(Role), dto.Role))
            throw new ApplicationException("Invalid Role");

        if (await EmailExistsAsync(dto.Email))
            throw new ApplicationException("Email already exists");

        if (await NickNameExistsAsync(dto.NickName))
            throw new ApplicationException("NickName already exists");

        if (!dto.Password.Equals(dto.ConfirmPassword))
            throw new ApplicationException("Password must be equals to Confirm Password");
    }
    
    private async Task ValidateUpdateUserAsync(User entity)
    {
        var savedEntity = await _repository.GetByIdAsync(entity.Id);
        if (savedEntity is null)
            throw new ApplicationException("User not found");
        
        if(entity.Email != savedEntity.Email)
            throw new ApplicationException("Cannot change email");
        
        if(await NickNameExistsAsync(entity.NickName))
            throw new ApplicationException("NickName already exists");
    }
}