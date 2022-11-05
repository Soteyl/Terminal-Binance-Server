using Ixcent.CryptoTerminal.Domain.AvailableExchanges.Models.Service;

namespace Ixcent.CryptoTerminal.Domain.AvailableExchanges.Models.Repository
{
    public class GetAvailableExchangesResult
    {
        public IEnumerable<AvailableExchange> AvailableExchanges { get; set; }
    }
}