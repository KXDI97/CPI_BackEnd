using System.ComponentModel.DataAnnotations;

namespace CPI_Backend.API.Models.Products
{
    public class Product
    {
        [Key]
        [Required]
        public string ProductId { get; set; }  = string.Empty;    // Código del producto (PK)

        [Required]
        public string Name { get; set; } = string.Empty;     // Nombre del producto

        [Required]
        
        public decimal Value { get; set; }        // Precio unitario

        [Required]
        public string Category { get; set; }   = string.Empty;    // Categoría

        [Required]
        public string Description { get; set; }  = string.Empty;  // Descripción

        [Required]
        public int Stock { get; set; }            // Inventario disponible
    }
}
