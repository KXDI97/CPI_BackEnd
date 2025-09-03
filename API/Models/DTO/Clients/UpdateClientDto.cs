using System.ComponentModel.DataAnnotations;

namespace CPI_Backend.API.DTO.Clients
{
    // DTO para actualizar cliente

public class UpdateClientDto
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(27)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [StringLength(15)]
        public string? PhoneNumber { get; set; }

        [StringLength(10)]
        public string RoleCode { get; set; } = string.Empty;
    }
}