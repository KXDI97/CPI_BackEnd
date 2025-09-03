using System.ComponentModel.DataAnnotations;

namespace CPI_Backend.API.DTO.Purchase
{
        public class TransactionDto
    {
        public int TransactionNumber { get; set; }
        public int OrderNumber { get; set; } 
        public int InvoiceNumber { get; set; } 
        public string Reminder { get; set; } = string.Empty;
        public string TransactionStatus { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }

    public class CreateTransactionDto
    {
        [Required]
        [StringLength(12)]
        public int OrderNumber { get; set; } 

        [Required]
        public int InvoiceNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string Reminder { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string TransactionStatus { get; set; } = string.Empty;

        [Required]
        public DateTime PaymentDate { get; set; }
    }

    public class UpdateTransactionStatusDto
    {
        [Required]
        [StringLength(20)]
        public string TransactionStatus { get; set; } = string.Empty;

        public DateTime? PaymentDate { get; set; }
    }

    public class TransactionSummaryDto
    {
        public int TransactionNumber { get; set; }
        public string TransactionStatus { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string ClientName { get; set; } = string.Empty;
    }

}