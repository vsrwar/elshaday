using System.ComponentModel.DataAnnotations;
using ElShaday.Domain.Entities.User;

namespace ElShaday.Application.DTOs.Requests;

public class UserRequestDto
{
    [Required]
    [MinLength(5)]
    [MaxLength(255)]
    public string Email { get; set; }
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string NickName { get; set; }
    [Required]
    [MinLength(8)]
    [MaxLength(15)]
    public string Password { get; set; }
    [Required]
    [MinLength(8)]
    [MaxLength(15)]
    public string ConfirmPassword { get; set; }
    [Required]
    public Role Role { get; set; }
}