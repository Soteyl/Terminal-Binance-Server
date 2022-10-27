namespace Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Repository
{
    public class RemoveTokenRequest
    {
        public string UserId { get; set; }
        
        public string Exchange { get; set; }
    }
}