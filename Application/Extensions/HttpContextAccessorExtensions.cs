using System.Security.Claims;

using Microsoft.AspNetCore.Http;

namespace Ixcent.CryptoTerminal.Application
{
    /// <summary>
    /// Extensions for <see cref="IHttpContextAccessor"/> implementations.
    /// </summary>
    public static class HttpContextAccessorExtensions
    {
        /// <summary>
        /// Access to a current user name identifier.
        /// </summary>
        /// <param name="contextAccessor">Accessor. Can be received via dependency injection.</param>
        /// <returns><see cref="string"/> with current user login or <see cref="null"/></returns>
        public static string? GetCurrentUserId(this IHttpContextAccessor contextAccessor)
        {
            return contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
