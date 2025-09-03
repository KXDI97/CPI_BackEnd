using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CPI_Backend.API.Models.Clients;
using CPI_Backend.API.Models.Products;
using CPI_Backend.API.Models.Purchasesc;

namespace CPI_Backend.API.Models.Purchasesc;

[Table("Transactions")]

public class Transaction
{
    [Key]   
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Transaction_Number")]
    public int TransactionNumber { get; set; } // Transaction number (PK)

    [Required]
    [Column("Order_Number")]
    public int OrderNumber { get; set; }  // FK to Purchase
    [ForeignKey(nameof(OrderNumber))]
    public Purchase Purchase { get; set; } = null!; // Navigation property

    [Required]
    [Column("Invoice_Number")]
    public int InvoiceNumber { get; set; }  // FK to Invoice
    [ForeignKey(nameof(InvoiceNumber))]
    public Invoice Invoice { get; set; } = null!; // Navigation property

    [Required]
    [Column("Reminder")]
    [StringLength(100)]
    public string Reminder { get; set; } = string.Empty; // 

    [Required]
    [Column("Transaction_Status")]
    [StringLength(20)]
    public string TransactionStatus { get; set; } = string.Empty; // Status of the transaction

    [Required]
    [Column("Payment_Date")]
    public DateTime PaymentDate { get; set; } // Date of the payment
    
} 