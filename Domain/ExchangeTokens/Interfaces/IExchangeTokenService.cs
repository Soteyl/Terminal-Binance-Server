using Ixcent.CryptoTerminal.Domain.Common.Models;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Service;

namespace Ixcent.CryptoTerminal.Domain.ExchangeTokens.Interfaces
{
    public interface IExchangeTokenService
    {
        Task<Response<GetTokensResponse>> Get(GetTokensRequest getTokensRequest, CancellationToken cancellationToken = default);

        Task<Response> Add(AddTokenRequest tokenRequest, CancellationToken cancellationToken = default);

        Task<Response> Remove(RemoveTokenRequest userExchange, CancellationToken cancellationToken = default);
    }
}