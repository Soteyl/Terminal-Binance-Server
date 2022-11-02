namespace Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Service
{
    public class RemoveTokenRequest
    {
        public string UserId { get; set; }
        
        public string Exchange { get; set; }
    }
}