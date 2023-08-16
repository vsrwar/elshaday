using System.ComponentModel.DataAnnotations;

namespace ElShaday.Application.DTOs.Requests;

public class AdminUserRequestDto
{
    [Required]
    [MinLength(5)]
    [MaxLength(255)]
    public string Email { get; set; }
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string NickName { get; set; }
}