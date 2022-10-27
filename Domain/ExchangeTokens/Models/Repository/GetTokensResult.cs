namespace Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Repository
{
    public class GetTokensResult
    {
        public IEnumerable<ExchangeToken> Tokens { get; set; }
    }
}