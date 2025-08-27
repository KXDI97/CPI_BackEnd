// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;

// namespace CPI_Backend.API.Models.Para_organizar;

// public class Client
// {
//     [Key]
//     [Column("Doc_Identidad")]
//     [StringLength(12)]
//     public required string DocIdentidad { get; set; }

//     [Required]
//     [Column("Nom_Usuario")]
//     [StringLength(50)]
//     public required string NomUsuario { get; set; }

//     [Required]
//     [Column("Correo")]
//     [StringLength(27)]
//     public required string Correo { get; set; }

//     [Column("Num_tel")]
//     [StringLength(15)]
//     public string? NumTel { get; set; } // Este sí es nullable

//     [Required]
//     [Column("Contrasenia")]
//     [StringLength(15)]
//     public required string Contrasenia { get; set; }

//     [Required]
//     [Column("Cod_Rol")]
//     [StringLength(10)]
//     public required string CodRol { get; set; }

//     // Relación con la tabla Roles (si la vas a mapear)
//     // public Rol Rol { get; set; } // ← Descomenta si también tienes la clase Rol
// }
