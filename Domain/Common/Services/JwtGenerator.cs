using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Ixcent.CryptoTerminal.Domain.Common.Configs;
using Ixcent.CryptoTerminal.Domain.Common.Interfaces;
using Ixcent.CryptoTerminal.Domain.Database;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Ixcent.CryptoTerminal.Domain.Common.Services
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
            List<Claim> claims = new() { new Claim(JwtRegisteredClaimNames.NameId, user.Id) };

            SigningCredentials credentials = new(_key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials
            };
            JwtSecurityTokenHandler tokenHandler = new();

            SecurityToken? token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
