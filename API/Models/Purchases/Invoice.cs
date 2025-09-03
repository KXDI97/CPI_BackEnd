using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CPI_Backend.API.Models.Clients;
using CPI_Backend.API.Models.Products;
using CPI_Backend.API.Models.Purchasesc;

namespace CPI_Backend.API.Models.Purchasesc;

[Table("Invoices")]

public class Invoice
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Invoice_Number")]
    public int InvoiceNumber { get; set; } // Invoice number (PK)

    [Required]
    [StringLength(12)]
    [Column("Identity_Doc")]
    public string IdentityDoc { get; set; } = null!; // FK to client
    [ForeignKey(nameof(IdentityDoc))]
    public Client Client { get; set; } = null!; // Navigation property

    [Required]
    [StringLength(50)]
    public string ProductId { get; set; } = null!; // FK to Product
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; } = null!; // Navigation property

    [Required]
    [Column("Order_Number")]
    public int OrderNumber { get; set; }  // FK to Purchase
    [ForeignKey(nameof(OrderNumber))]
    public Purchase Purchase { get; set; } = null!; // Navigation property
    
    [Required]
    [Column("Subtotal")]
    public decimal Subtotal { get; set; } // Subtotal amount

    [Required]
    [Column("VAT")]
    public decimal Vat { get; set; } // VAT amount]
 
    [Required]
    [Column("Exchange_Rate")]
    public decimal ExchangeRate { get; set; } // Exchange rate at the time of purchase

    [Required]
    [Column("Total")]
    public decimal Total { get; set; } // Total amount

    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>(); // Relación con las transacciones

}