using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Repository;

namespace Ixcent.CryptoTerminal.Domain.ExchangeTokens.Interfaces
{
    public interface IExchangeTokenRepository
    {
        Task<IEnumerable<ExchangeToken>> GetTokensByUserId(string userId);

        Task AddToken(string userId, ExchangeToken token);
        
        /// <exception cref="InvalidOperationException">Couldn't find a token</exception>
        Task RemoveToken(string userId, string exchange);
    }
}