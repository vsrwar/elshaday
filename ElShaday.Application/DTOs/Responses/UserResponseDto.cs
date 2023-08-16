using ElShaday.Domain.Entities.User;

namespace ElShaday.Application.DTOs.Responses;

public class UserResponseDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string NickName { get; set; }
    public bool Active { get; set; }
    public Role Role { get; set; }
}