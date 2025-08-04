using System.ComponentModel.DataAnnotations;

namespace ApiAutenticacion.Models;

public class UsuarioLoginDto
{
    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "El formato del correo no es válido")]
    public string Correo { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}