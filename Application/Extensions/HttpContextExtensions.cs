using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace Application.Extensions;

public  static class HttpContextExtensions
{
    public static string? GetAuthorizationToken(this HttpContext context)
    {
        return context.Request.Headers.Authorization
            .FirstOrDefault()?.Split(' ').LastOrDefault();
    }

    public static Guid? GetUserIdFromClaims(this HttpContext context)
    {
        var userId = context.User
            .Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType)?.Value;
        return Guid.TryParse(userId, out var userGuid) ? userGuid : null ;
    }
}