using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAutenticacion.Models;

[Table("usuarios")] // Asegúrate que coincida con el nombre de tu tabla
public class Usuario
{
    [Key]
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Required]
    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; }

    [Required]
    [Column("cedula")]
    [StringLength(15)]
    public string Cedula { get; set; }

    [Required]
    [EmailAddress]
    [Column("correo")]
    [StringLength(100)]
    public string Correo { get; set; }

    [Column("telefono")]
    [StringLength(20)]
    public string? Telefono { get; set; }

    [Required]
    [Column("tipo_usuario")]
    public string TipoUsuario { get; set; } // "comprador", "vendedor", "mecanico"

    [Column("direccion")]
    public string? Direccion { get; set; }

    [Column("creado_en")]
    public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

    [Required]
    [Column("password_hash")]
    public string PasswordHash { get; set; }

    [Required]
    [Column("password_salt")]
    public string PasswordSalt { get; set; }
}