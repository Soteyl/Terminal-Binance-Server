using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CryptoTerminal.Models.Auth
{
    public class AuthOptions
    {
        private const string _key = "vC!iHi$uxZPGeJWY3&h532lL140";

        public static string Issuer { get; } = "Ixcent";

        public static string Audience { get; } = "Client";

        public static TimeSpan LifeTime { get; } = new TimeSpan(30, 0, 0, 0);

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_key));
        }
    }
}
