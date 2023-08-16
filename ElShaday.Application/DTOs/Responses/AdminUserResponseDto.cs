namespace ElShaday.Application.DTOs.Responses;

public class AdminUserResponseDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string NickName { get; set; }
    public bool Active { get; set; }
}