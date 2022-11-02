using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Repository;

namespace Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Service
{
    public class AddTokenRequest
    {
        public string UserId { get; set; } = string.Empty;
        
        public string Exchange { get; set; }
        
        public string Key { get; set; }
        
        public string Secret { get; set; }
    }
}