using Ixcent.CryptoTerminal.Domain.Common.Models;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Service;

namespace Ixcent.CryptoTerminal.Domain.ExchangeTokens.Interfaces
{
    public interface IExchangeTokenService
    {
        Task<Response<IEnumerable<CheckedExchangeToken>>> GetTokensByUserId(string userId,
            CancellationToken cancellationToken = default);

        Task<Response> AddToken(UserExchangeToken token, CancellationToken cancellationToken = default);

        Task<Response> RemoveToken(UserExchange userExchange, CancellationToken cancellationToken = default);
    }
}