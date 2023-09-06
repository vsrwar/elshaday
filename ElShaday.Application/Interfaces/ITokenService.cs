using ElShaday.Application.DTOs.Responses;

namespace ElShaday.Application.Interfaces;

public interface ITokenService
{
    string GenerateTokenAsync(UserResponseDto userResponseDto);
}