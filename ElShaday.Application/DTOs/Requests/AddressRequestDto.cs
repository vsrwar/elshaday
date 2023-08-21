using System.ComponentModel.DataAnnotations;

namespace ElShaday.Application.DTOs.Requests;

public class AddressRequestDto
{
    [Required] public string Cep { get; set; }
    [Required] public string Logradouro { get; set; }
    public string Complemento { get; set; }
    [Required] public string Bairro { get; set; }
    [Required] public string Localidade { get; set; }
    [Required] public string Uf { get; set; }
    public string Ibge { get; set; }
    public string Gia { get; set; }
    public string Ddd { get; set; }
    public string Siafi { get; set; }
    [Required] public string Numero { get; set; }
}