using Ixcent.CryptoTerminal.Domain.Database;

namespace Ixcent.CryptoTerminal.Domain.Common.Interfaces
{
    public interface IJwtGenerator
    {
        string CreateToken(AppUser user);
    }
}
