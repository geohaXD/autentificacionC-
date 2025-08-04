using ApiAutenticacion.Models;
using ApiAutenticacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiAutenticacion.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("registro")]
    public async Task<IActionResult> Registrar([FromBody] UsuarioRegistroDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var usuario = new Usuario
        {
            Nombre = request.Nombre,
            Cedula = request.Cedula,
            Correo = request.Correo,
            TipoUsuario = request.TipoUsuario,
            Telefono = request.Telefono,
            Direccion = request.Direccion
        };

        var resultado = await _authService.Registrar(usuario, request.Password);
        
        return resultado 
            ? Ok(new { Mensaje = "Usuario registrado exitosamente" }) 
            : BadRequest(new { Mensaje = "El correo o cédula ya están registrados" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UsuarioLoginDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var token = await _authService.Login(request.Correo, request.Password);
        
        return token != null 
            ? Ok(new { 
                Token = token,
                Mensaje = "Autenticación exitosa" 
              }) 
            : Unauthorized(new { Mensaje = "Credenciales inválidas" });
    }
}