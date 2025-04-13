using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Options;

public static class AuthOptions
{
    public static string Issuer { get; set; } = Environment.GetEnvironmentVariable("ISSUER")!;
    public static string Audience { get; set; } = Environment.GetEnvironmentVariable("AUDIENCE")!;
    public static string Key { get; set; } = Environment.GetEnvironmentVariable("SECRET_KEY")!;

    public static int AccessLifetime { get; set; } =
        int.Parse(Environment.GetEnvironmentVariable("ACCESS_TOKEN_LIFETIME")!);

    public static int RefreshLifetime { get; set; } = int.Parse(Environment.GetEnvironmentVariable("REFRESH_TOKEN_LIFETIME")!);


    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }
}