using System.Text;

using Microsoft.IdentityModel.Tokens;

namespace Ixcent.CryptoTerminal.Domain.Common.Configs
{
    /// <summary>
    /// Data class for JWT tokens generation.
    /// </summary>
    public class AuthOptions
    {
        private const string _key = "vC!iHi$uxZPGeJWY3&h532lL140";

        /// <summary>
        /// For <see cref="TokenValidationParameters.ValidIssuer"/>
        /// </summary>
        public static string Issuer { get; } = "Ixcent";

        /// <summary>
        /// For <see cref="TokenValidationParameters.ValidAudience"/>
        /// </summary>
        public static string Audience { get; } = "Client";

        /// <summary>
        /// For <see cref=""/>
        /// </summary>
        public static TimeSpan LifeTime { get; } = new(30, 0, 0, 0);

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_key));
        }
    }
}
