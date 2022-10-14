using Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Models;
using Ixcent.CryptoTerminal.Application.Mediatr;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Services
{
    public interface IExchangeTokenService
    {
        Task<Response<IEnumerable<CheckedExchangeToken>>> GetTokensByUserId(string userId);

        Task<Response> AddToken(UserExchangeToken token);

        Task<Response> RemoveToken(UserExchange userExchange);
    }
}