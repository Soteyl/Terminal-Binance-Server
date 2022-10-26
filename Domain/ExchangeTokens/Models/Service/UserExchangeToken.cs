using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Repository;

namespace Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Service
{
    public class UserExchangeToken: ExchangeToken
    {
        public string UserId { get; set; } = string.Empty;
    }
}