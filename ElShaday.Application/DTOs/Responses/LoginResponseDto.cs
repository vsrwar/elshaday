namespace ElShaday.Application.DTOs.Responses;

public class LoginResponseDto
{
    public UserResponseDto User { get; private set; }
    public string Token { get; private set; }
    
    public LoginResponseDto(UserResponseDto user, string token)
    {
        User = user;
        Token = token;
    }
}