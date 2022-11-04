namespace Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Controller
{
    public class RemoveExchangeTokenQuery
    {
        /// <summary>
        /// Crypto exchange token key
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Crypto exchange token secret
        /// </summary>
        public string Secret { get; set; } = string.Empty;

        /// <summary>
        /// Crypto exchange name
        /// </summary>
        public string Exchange { get; set; } = string.Empty;
    }
}