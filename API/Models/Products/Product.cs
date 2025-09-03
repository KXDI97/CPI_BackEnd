using System.ComponentModel.DataAnnotations;
using CPI_Backend.API.Models.Purchasesc;
namespace CPI_Backend.API.Models.Products
{
    public class Product
    {
        [Key]
        [Required]
        public string ProductId { get; set; } = string.Empty;    // Código del producto (PK)

        [Required]
        public string Name { get; set; } = string.Empty;     // Nombre del producto

        [Required]

        public decimal Value { get; set; }        // Precio unitario

        [Required]
        public string Category { get; set; } = string.Empty;    // Categoría

        [Required]
        public string Description { get; set; } = string.Empty;  // Descripción

        [Required]
        public int Stock { get; set; }            // Inventario disponible
      public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();// Relación con las compras

        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>(); // Relación con las facturas
   
    }
}
