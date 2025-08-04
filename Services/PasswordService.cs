using System.Security.Cryptography;
using System.Text;

namespace ApiAutenticacion.Services;

public class PasswordService
{
    public (string Hash, string Salt) CreatePasswordHash(string password)
    {
        using var hmac = new HMACSHA512();
        var salt = hmac.Key;
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return (
            Hash: Convert.ToBase64String(hash),
            Salt: Convert.ToBase64String(salt)
        );
    }

    public bool VerifyPassword(string password, string storedHash, string storedSalt)
    {
        var hashBytes = Convert.FromBase64String(storedHash);
        var saltBytes = Convert.FromBase64String(storedSalt);

        using var hmac = new HMACSHA512(saltBytes);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(hashBytes);
    }
}