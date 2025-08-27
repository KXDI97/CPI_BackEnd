// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;

// namespace CPI_Backend.API.Models.Para_organizar
// {
//     public class Compra
//     {
//         [Key]
//         public required string No_Orden { get; set; }

//         [Required]
//         [ForeignKey("Cliente")]
//         public required string Doc_Identidad { get; set; }

//         public DateTime Fecha_Compra { get; set; }
//         public required string Cod_Prod { get; set; }
//         public required string Ref_Prod { get; set; }
//         public int Cantidad { get; set; }
//         public double Valor_Prod { get; set; }
//         public double? TRM { get; set; }

//         public required Client Clients { get; set; }
//     }
// }
