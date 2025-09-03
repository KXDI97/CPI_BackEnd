using System.ComponentModel.DataAnnotations;

namespace CPI_Backend.API.DTO.Products
{
    public class ProductDto
    {
        public string ProductId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Stock { get; set; }
    }
}