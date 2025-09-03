using System.ComponentModel.DataAnnotations;

namespace CPI_Backend.API.DTO.Products
{
    public class UpdateProductDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El valor debe ser mayor a 0")]
        public decimal Value { get; set; }

        [Required]
        public string Category { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser mayor o igual a 0")]
        public int Stock { get; set; }
    }

    // DTO simplificado para listas
    public class ProductSummaryDto
    {
        public string ProductId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public string Category { get; set; } = string.Empty;
        public int Stock { get; set; }
    }
}