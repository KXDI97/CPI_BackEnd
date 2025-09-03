using System.ComponentModel.DataAnnotations;

namespace CPI_Backend.API.DTO.Clients;
// DTO para mostrar información básica del cliente
    public class ClientDto
    {
        public string IdentityDoc { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string RoleCode { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
    }