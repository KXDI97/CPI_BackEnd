using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CPI_Backend.API.Models.Purchasesc;

namespace CPI_Backend.API.Models.Clients;

[Table("Roles")]
public class Role
{
    [Key]
    [Column("Role_Code")]
    [StringLength(10)]
    public required string RoleCode { get; set; } // Código de rol (PK)

    [Required]
    [Column("Role_Name")]
    [StringLength(10)]
    public required string RoleName { get; set; } // Nombre del rol
    public ICollection<Client> Clients { get; set; } = new List<Client>(); // Relación con los clientes
}