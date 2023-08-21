using System.ComponentModel.DataAnnotations;
using ElShaday.Domain.Entities.User;

namespace ElShaday.Application.DTOs.Requests;

public class UserEditRequestDto
{
    [Required]
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? NickName { get; set; }
    public Role? Role { get; set; }
    public bool? Active { get; set; }
}