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
        entity.Activate();
        await _repository.CreateAsync(entity);
        return _mapper.Map<UserResponseDto>(entity);
    }

    public async Task<UserResponseDto> UpdateAsync(UserEditRequestDto requestDto)
    {
        var entity = await ValidateUpdateUserAsync(requestDto);
        entity.Update(requestDto.Email, requestDto.NickName, requestDto.Role, requestDto.Active);
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
        await _repository.DeleteAsync(id);
    }

    public async Task<bool> EmailExistsAsync(string email) => await _repository.EmailExistsAsync(email);

    public async Task<bool> NickNameExistsAsync(int? selfId, string nickName) => await _repository.NickNameExistsAsync(selfId, nickName);

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
        
        entity.Activate();
        await _repository.UpdateAsync(entity);
    }

    public async Task<int> CountActivesAsync()
        => await _repository.CountActivesAsync();

    private async Task ValidateNewUserAsync(UserRequestDto dto)
    {
        if (!Enum.IsDefined(typeof(Role), dto.Role))
            throw new ApplicationException("Invalid Role");

        if (await EmailExistsAsync(dto.Email))
            throw new ApplicationException("Email already exists");

        if (await NickNameExistsAsync(dto.Id, dto.NickName))
            throw new ApplicationException("NickName already exists");

        if (!dto.Password.Equals(dto.ConfirmPassword))
            throw new ApplicationException("Password must be equals to Confirm Password");
    }
    
    private async Task<User> ValidateUpdateUserAsync(UserEditRequestDto request)
    {
        var savedEntity = await _repository.GetByIdAsync(request.Id);
        if (savedEntity is null)
            throw new ApplicationException("User not found");
        
        if(request.Email != savedEntity.Email)
            throw new ApplicationException("Cannot change email");

        if(string.IsNullOrEmpty(request.NickName))
            throw new ApplicationException("NickName cannot be empty");

        if(await NickNameExistsAsync(request.Id, request.NickName))
            throw new ApplicationException("NickName already exists");

        return savedEntity;
    }
}