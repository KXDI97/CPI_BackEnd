using System.ComponentModel.DataAnnotations;

namespace CPI_Backend.API.DTO.Clients
{

// DTO para crear/actualizar cliente
    public class CreateClientDto
    {
        [Required]
        [StringLength(12)]
        public string IdentityDoc { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(27)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [StringLength(15)]
        public string? PhoneNumber { get; set; }

        [Required]
        [StringLength(250)]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string RoleCode { get; set; } = string.Empty;
    }
}