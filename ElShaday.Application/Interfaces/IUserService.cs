using ElShaday.Application.DTOs.Requests;
using ElShaday.Application.DTOs.Responses;
using ElShaday.Domain.Interfaces;

namespace ElShaday.Application.Interfaces;

public interface IUserService : ICrudService<UserRequestDto, UserResponseDto>, IEditable<UserEditRequestDto, UserResponseDto>
{
    Task<bool> EmailExistsAsync(string email);
    Task<bool> NickNameExistsAsync(int? selfId, string nickName);
    Task DeactivateAsync(int id);
    Task ActivateAsync(int id);
    Task<int> CountActivesAsync();
    Task<UserResponseDto> VerifyLoginAsync(LoginRequestDto loginRequestDto);
    Task<bool> CanChangePasswordAsync(string nickName);
    Task<bool> ChangePasswordAsync(ChangeUserPasswordDto changeUserPasswordDto);
}