namespace Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Repository
{
    public class AddTokenRequest
    {
        public string UserId { get; set; }
        
        public string Exchange { get; set; }
        
        public Token Token { get; set; }

    }
}