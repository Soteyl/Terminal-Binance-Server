namespace Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Models
{
    public class ExchangeTokensResult
    {
        public Dictionary<string, List<string>> AvailableExchanges { get; set; } = new Dictionary<string, List<string>>();

    }
}
