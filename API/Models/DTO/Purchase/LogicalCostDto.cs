using System.ComponentModel.DataAnnotations;

namespace CPI_Backend.API.DTO.Purchase
{
    public class LogicalCostDto
    {
        public int OrderNumber { get; set; }
        public decimal InternationalTransport { get; set; }
        public decimal LocalTransport { get; set; }
        public decimal Nationalization { get; set; }
        public decimal CargoInsurance { get; set; }
        public decimal Storage { get; set; }
        public decimal Others { get; set; }
        public decimal Total => InternationalTransport + LocalTransport + Nationalization + CargoInsurance + Storage + Others;
    }

    public class CreateLogicalCostDto
    {
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El valor debe ser mayor o igual a 0")]
        public decimal InternationalTransport { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El valor debe ser mayor o igual a 0")]
        public decimal LocalTransport { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El valor debe ser mayor o igual a 0")]
        public decimal Nationalization { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El valor debe ser mayor o igual a 0")]
        public decimal CargoInsurance { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El valor debe ser mayor o igual a 0")]
        public decimal Storage { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El valor debe ser mayor o igual a 0")]
        public decimal Others { get; set; }
    }
}

