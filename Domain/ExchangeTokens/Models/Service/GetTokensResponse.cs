namespace Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Service
{
    public class GetTokensResponse
    {
        public IEnumerable<CheckedExchangeToken> Tokens { get; set; }
    }
}