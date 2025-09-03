using System.ComponentModel.DataAnnotations;

namespace CPI_Backend.API.DTO.Purchase
{
     public class PurchaseDto
    {
        public int OrderNumber { get; set; }
        public string IdentityDoc { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public DateTime PurchaseDate { get; set; }
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string PurchaseReference { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal ProductValue { get; set; }
        public decimal ExchangeRate { get; set; }
        public LogicalCostDto? LogicalCosts { get; set; }
    }

    public class CreatePurchaseDto
    {
        [Required]
        [StringLength(12)]
        public string IdentityDoc { get; set; } = string.Empty;

        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductId { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string PurchaseReference { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El valor del producto debe ser mayor a 0")]
        public decimal ProductValue { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "La tasa de cambio debe ser mayor a 0")]
        public decimal ExchangeRate { get; set; }

        public CreateLogicalCostDto? LogicalCosts { get; set; }
    }

    public class PurchaseSummaryDto
    {
        public int OrderNumber { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public DateTime PurchaseDate { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal ProductValue { get; set; }
        public decimal Total { get; set; }
    }
}