using System.ComponentModel.DataAnnotations;

namespace CPI_Backend.API.Models.DTO;

public class LoginDTO
{
    [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    public string Password { get; set; } = string.Empty;
}
