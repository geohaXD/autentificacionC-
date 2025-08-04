using ApiAutenticacion.Data;
using ApiAutenticacion.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiAutenticacion.Services;

public class AuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;
    private readonly PasswordService _passwordService;

    public AuthService(AppDbContext context, IConfiguration config, PasswordService passwordService)
    {
        _context = context;
        _config = config;
        _passwordService = passwordService;
    }

    public async Task<bool> Registrar(Usuario usuario, string password)
    {
        if (await _context.Usuarios.AnyAsync(u => u.Correo == usuario.Correo))
            return false;

        // Generar hash y salt
        var (hash, salt) = _passwordService.CreatePasswordHash(password);
        usuario.PasswordHash = hash;
        usuario.PasswordSalt = salt;

        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<string?> Login(string correo, string password)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Correo == correo);

        if (usuario == null || 
            !_passwordService.VerifyPassword(password, usuario.PasswordHash, usuario.PasswordSalt))
            return null;

        return GenerarToken(usuario);
    }

    private string GenerarToken(Usuario usuario)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(
            securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
            new Claim(ClaimTypes.Email, usuario.Correo),
            new Claim(ClaimTypes.Role, usuario.TipoUsuario)
        };

        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}