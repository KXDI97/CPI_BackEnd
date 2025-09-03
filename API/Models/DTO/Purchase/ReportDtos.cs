using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

// DTOs para reportes y consultas complejas
namespace CPI_Backend.API.DTO.Purchase
{
    public class PurchaseReportDto
    {
        public int OrderNumber { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public DateTime PurchaseDate { get; set; }
        public int Quantity { get; set; }
        public decimal ProductValue { get; set; }
        public decimal LogicalCostsTotal { get; set; }
        public decimal InvoiceTotal { get; set; }
        public string TransactionStatus { get; set; } = string.Empty;
    }

    public class ClientPurchaseHistoryDto
    {
        public string IdentityDoc { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public List<PurchaseSummaryDto> Purchases { get; set; } = new();
        public decimal TotalPurchases { get; set; }
        public int TotalOrders { get; set; }
    }

    public class ProductSalesReportDto
    {
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int TotalQuantitySold { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalOrders { get; set; }
        public decimal AverageOrderValue { get; set; }
    }

    public class FinancialSummaryDto
    {
        public decimal TotalSales { get; set; }
        public decimal TotalVAT { get; set; }
        public decimal TotalLogicalCosts { get; set; }
        public decimal NetRevenue { get; set; }
        public int TotalTransactions { get; set; }
        public int PendingTransactions { get; set; }
        public int CompletedTransactions { get; set; }
    }
}