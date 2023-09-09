using System.ComponentModel.DataAnnotations;

namespace ElShaday.Application.DTOs.Requests;

public class ChangeUserPasswordDto
{
    [Required]
    public string NickName { get; set; }
    [Required]
    [MinLength(8)]
    [MaxLength(15)]
    public string Password { get; set; }
    [Required]
    [MinLength(8)]
    [MaxLength(15)]
    public string ConfirmPassword { get; set; }
}