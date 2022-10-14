namespace Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Models
{
    public class CheckedExchangeToken
    {
        public string Key { get; set; }
        
        public string Secret { get; set; }
        
        public string Exchange { get; set; }
        
        /// <summary> Access to services that this API-key gives </summary>
        public IEnumerable<string> AvailableServices { get; set; }
    }
}