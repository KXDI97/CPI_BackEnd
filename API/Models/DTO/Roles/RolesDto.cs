using System.ComponentModel.DataAnnotations;

namespace CPI_Backend.API.DTO.Roles
{
    public class RoleDto
    {
        public string RoleCode { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
    }

    public class CreateRoleDto
    {
        [Required]
        [StringLength(10)]
        public string RoleCode { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string RoleName { get; set; } = string.Empty;
    }
}