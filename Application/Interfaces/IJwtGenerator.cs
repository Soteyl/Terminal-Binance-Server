namespace Ixcent.CryptoTerminal.Application.Interfaces
{
    using Domain.Database;

    public interface IJwtGenerator
    {
        string CreateToken(AppUser user);
    }
}
