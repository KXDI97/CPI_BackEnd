using System.ComponentModel.DataAnnotations;

namespace CPI_Backend.API.DTO.Purchase
{
    public class InvoiceDto
    {
        public int InvoiceNumber { get; set; }
        public string IdentityDoc { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int OrderNumber { get; set; } 
        public decimal Subtotal { get; set; }
        public decimal Vat { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal Total { get; set; }
    }

    public class CreateInvoiceDto
    {
        [Required]
        [StringLength(12)]
        public string IdentityDoc { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string ProductId { get; set; } = string.Empty;

        [Required]
        public int OrderNumber { get; set; } 

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El subtotal debe ser mayor a 0")]
        public decimal Subtotal { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El IVA debe ser mayor o igual a 0")]
        public decimal Vat { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "La tasa de cambio debe ser mayor a 0")]
        public decimal ExchangeRate { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El total debe ser mayor a 0")]
        public decimal Total { get; set; }
    }

    public class InvoiceSummaryDto
    {
        public int InvoiceNumber { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}