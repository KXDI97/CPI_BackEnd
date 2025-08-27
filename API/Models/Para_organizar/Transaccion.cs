using System.ComponentModel.DataAnnotations;
namespace APIUsuarios.Models
{
    public class Transaccion
    {
        [Key]
        public required string Num_Transaccion { get; set; }
        public required string No_Orden { get; set; }
        public required string Num_Factura { get; set; }
        public required string Recordatorio { get; set; }
        public required string Estado_Transac { get; set; }
        public DateTime Fechas_Pago { get; set; }
    }
}
