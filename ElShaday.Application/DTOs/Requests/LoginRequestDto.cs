using System.ComponentModel.DataAnnotations;

namespace ElShaday.Application.DTOs.Requests;

public class LoginRequestDto
{
    [Required] public string NickName { get; set; }
    [Required] public string Password { get; set; }
}