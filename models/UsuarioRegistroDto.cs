using System.ComponentModel.DataAnnotations;

namespace ApiAutenticacion.Models;

public class UsuarioRegistroDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "La cédula es obligatoria")]
    [StringLength(15, ErrorMessage = "La cédula no puede exceder los 15 caracteres")]
    public string Cedula { get; set; }

    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "El formato del correo no es válido")]
    [StringLength(100, ErrorMessage = "El correo no puede exceder los 100 caracteres")]
    public string Correo { get; set; }

    [Required(ErrorMessage = "El tipo de usuario es obligatorio")]
    public string TipoUsuario { get; set; }

    [Phone(ErrorMessage = "Formato de teléfono inválido")]
    [StringLength(20, ErrorMessage = "El teléfono no puede exceder los 20 caracteres")]
    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "La confirmación de contraseña es obligatoria")]
    [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}