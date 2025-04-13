using System.Security.Cryptography;
using System.Text;
using Application.Interfaces.Services;

namespace Infrastructure.Services;

public class PasswordService(SHA256 sha) : IPasswordService
{
    public string HashPassword(string password)
    {
        var bytes = Encoding.Unicode.GetBytes(password);

        var result = sha.ComputeHash(bytes);

        var hash = Encoding.Unicode.GetString(result);

        return hash;
    }
}