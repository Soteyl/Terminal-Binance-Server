using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Ixcent.CryptoTerminal.Application.Interfaces;
using Ixcent.CryptoTerminal.Domain.Auth;
using Ixcent.CryptoTerminal.Domain.Database;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Ixcent.CryptoTerminal.Infrastructure
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly SymmetricSecurityKey _key;

        public JwtGenerator(IConfiguration config)
        {
            _key = AuthOptions.GetSymmetricSecurityKey(); //TODO new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim> { new Claim(JwtRegisteredClaimNames.NameId, user.Id) };

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
