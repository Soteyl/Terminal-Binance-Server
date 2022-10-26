using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Repository;

namespace Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Service
{
    public class CheckedExchangeToken: ExchangeToken
    {
        /// <summary> Access to services that this API-key gives </summary>
        public IEnumerable<string> AvailableServices { get; set; }
    }
}