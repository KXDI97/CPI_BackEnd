using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CPI_Backend.API.Models.Clients;
using CPI_Backend.API.Models.Products;
using CPI_Backend.API.Models.Purchasesc;

namespace CPI_Backend.API.Models.Purchasesc;

[Table("LogicalCosts")]

public class LogicalCost
{
    [Key]
    [ForeignKey(nameof(Purchase))]
    [Column("Order_Number")]
    public int OrderNumber { get; set; } // Order number (PK)

    [Required]
    [Column("International_Transport")]
    public decimal InternationalTransport { get; set; } // Cost of international transport
    
    [Required]
    [Column("Local_Transport")]
    public decimal LocalTransport { get; set; } // Cost of local transport
    
    [Required]
    [Column("Nationalization")]
    public decimal Nationalization { get; set; } // Cost of nationalization

    [Required]
    [Column("Cargo_Insurance")]
    public decimal CargoInsurance { get; set; } // Cost of cargo insurance

    [Required]
    [Column("Storage")]
    public decimal Storage { get; set; } // Cost of storage

    [Required]
    [Column("Others")]
    public decimal Others { get; set; } // Other costs

    public Purchase Purchase { get; set; } = null!; // Navigation property for purchase

}
