using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ElShaday.Application.DTOs.Responses;
using ElShaday.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace ElShaday.API.Configuration;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateTokenAsync(UserResponseDto userResponseDto)
    {
        var jwtConfiguration = _configuration.GetSection("JwtConfigurations").Get<JwtConfiguration>();
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtConfiguration.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new []
            {
                new Claim(ClaimTypes.Email, userResponseDto.Email),
                new Claim(ClaimTypes.Role, userResponseDto.RoleString()),
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Audience = jwtConfiguration.Audience,
            Issuer = jwtConfiguration.Issuer
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}