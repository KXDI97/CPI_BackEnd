using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CPI_Backend.API.Models.Purchasesc;

namespace CPI_Backend.API.Models.Clients;

[Table("Clients")]
public class Client
{
    [Key]
    [Column("Identity_Doc")]
    [StringLength(12)]
    public required string IdentityDoc { get; set; }  // Documento de identidad (PK)

    [Required]
    [Column("Username")]
    [StringLength(50)]
    public required string Username { get; set; } // Nombre de usuario

    [Required]
    [Column("Email")]
    [StringLength(27)]
    public required string Email { get; set; } // Correo electrónico

    [Column("Phone_Number")]
    [StringLength(15)]
    public string? PhoneNumber { get; set; } // Este sí es nullable

    [Required]
    [Column("Password")]
    [StringLength(250)]
    public string Password { get; set; } = null!;// Contraseña

    [Required]
    [StringLength(10)]
    [Column("Role_Code")]
    public string RoleCode { get; set; } = null!; // FK

    [ForeignKey(nameof(RoleCode))]
    public Role Role { get; set; } = null!; // Navigation property

    public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>(); // Relación con las compras

    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>(); // Relación con las facturas
}
