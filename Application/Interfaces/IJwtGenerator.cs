using Ixcent.CryptoTerminal.Domain.Database;

namespace Ixcent.CryptoTerminal.Application.Interfaces
{
    public interface IJwtGenerator
    {
        string CreateToken(AppUser user);
    }
}
