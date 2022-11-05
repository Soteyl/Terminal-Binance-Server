namespace Ixcent.CryptoTerminal.Domain.AvailableExchanges.Models.Service
{
    public class GetAvailableExchangeResponse
    {
        public IEnumerable<AvailableExchange> AvailableExchanges { get; set; }
    }
}