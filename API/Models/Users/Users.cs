using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPI_Backend.API.Models.Users;

[Table("Users")]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID")]
    public int Id { get; set; }

    [Required]
    [Column("Username")]
    [StringLength(50, ErrorMessage = "El nombre de usuario no puede superar los 50 caracteres")]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress(ErrorMessage = "Correo electrónico inválido")]
    [Column("Email")]
    [StringLength(50)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(15, ErrorMessage = "La contraseña no debe superar los 15 caracteres")]
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
    [RegularExpression(
        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z\d]).+$",
        ErrorMessage = "La contraseña debe incluir mayúsculas, minúsculas, números y caracteres especiales."
    )]
    public string Password { get; set; } = string.Empty;

    [NotMapped] //No se guarda en la base de datos
    [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required]
    [Column("Role")]
    [StringLength(10)]
    public string Role { get; set; } = string.Empty;
}
