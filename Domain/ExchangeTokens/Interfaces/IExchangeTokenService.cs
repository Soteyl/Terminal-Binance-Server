using Ixcent.CryptoTerminal.Domain.Common.Models;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Service;

namespace Ixcent.CryptoTerminal.Domain.ExchangeTokens.Interfaces
{
    public interface IExchangeTokenService
    {
        Task<Response<IEnumerable<CheckedExchangeToken>>> GetTokensByUserId(string userId);

        Task<Response> AddToken(UserExchangeToken token);

        Task<Response> RemoveToken(UserExchange userExchange);
    }
}