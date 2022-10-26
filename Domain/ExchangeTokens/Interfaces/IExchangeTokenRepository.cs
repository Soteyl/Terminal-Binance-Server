using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Repository;

namespace Ixcent.CryptoTerminal.Domain.ExchangeTokens.Interfaces
{
    public interface IExchangeTokenRepository
    {
        Task<IEnumerable<ExchangeToken>> GetTokensByUserId(string userId, CancellationToken cancellationToken = default);

        Task AddToken(string userId, ExchangeToken token, CancellationToken cancellationToken = default);
        
        /// <exception cref="InvalidOperationException">Couldn't find a token</exception>
        Task RemoveToken(string userId, string exchange, CancellationToken cancellationToken = default);
    }
}