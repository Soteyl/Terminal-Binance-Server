using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Repository;

namespace Ixcent.CryptoTerminal.Domain.ExchangeTokens.Interfaces
{
    public interface IExchangeTokenRepository
    {
        Task<GetTokensResult> Get(GetTokensRequest request, CancellationToken cancellationToken = default);

        Task<GetOneTokenResult> GetOne(GetOneTokenRequest request, CancellationToken cancellationToken = default);

        Task Add(AddTokenRequest request, CancellationToken cancellationToken = default);
        
        /// <exception cref="InvalidOperationException">Couldn't find a token</exception>
        Task Remove(RemoveTokenRequest request, CancellationToken cancellationToken = default);
    }
}