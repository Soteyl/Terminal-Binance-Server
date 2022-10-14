namespace Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Models
{
    /// <summary>
    /// Result of getting available exchanges
    /// </summary>
    public class ExchangeTokensResult
    {
        /// <summary>
        /// A dictionary contains available exchanges and their functionality. <para/>
        /// Example: <br/>
        /// <code>
        /// {
        ///     "Binance": [ "spot", "margin", "futures" ]
        /// }
        /// </code>
        /// </summary>
        public Dictionary<string, IEnumerable<string>> AvailableExchanges { get; set; }
            = new();
    }
}
