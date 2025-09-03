using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CPI_Backend.API.Models.Clients;
using CPI_Backend.API.Models.Purchasesc;
using CPI_Backend.API.Models.Products;

namespace CPI_Backend.API.Models.Purchasesc;

[Table("Purchases")]
public class Purchase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Order_Number")]
    public int OrderNumber { get; set; }

    public LogicalCost? LogicalCost { get; set; } = null!; // Navigation property for logical costs

    [Required]
    [StringLength(12)]
    [Column("Identity_Doc")]
    public string IdentityDoc { get; set; } = null!; // FK to client
    [ForeignKey(nameof(IdentityDoc))]
    public Client Client { get; set; } = null!; // Navigation property


    [Required]
    [Column("Purchase_Date")]
    public DateTime PurchaseDate { get; set; } // Fecha de compra

    [Required]
    [StringLength(50)]
    [Column("ProductId")]
    public string ProductId { get; set; } = null!; // FK to Product
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; } = null!; // Navigation property

    [Required]
    [StringLength(50)]
    [Column("Product_Reference")]
    public string ProductReference { get; set; } = string.Empty;

    [Required]
    [Column("Quantity")]
    public int Quantity { get; set; }

    [Required]
    [Column("Product_Value")]
    public decimal ProductValue { get; set; }

    [Required]
    [Column("Exchange_Rate")]
    public decimal ExchangeRate { get; set; }

    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();// Relación con las facturas

    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>(); // Relación con las transacciones


}
