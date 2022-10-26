namespace Ixcent.CryptoTerminal.Domain.Exchanges.Common.Models
{
    public class CommonOrder
    {
        public string Id { get; set; }
        
        public string Symbol { get; set; }
        
        public decimal Price { get; set; }
        
        public decimal Quantity { get; set; }
        
        public string Status { get; set; }
    }
}