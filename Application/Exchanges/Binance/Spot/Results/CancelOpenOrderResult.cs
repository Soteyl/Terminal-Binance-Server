
using Binance.Net.Objects.Spot.SpotData;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Results
{
    /// <summary>
    /// Result returned after trying to cancel binance spot order.
    /// </summary>
    public class CancelOpenOrderResult
    {
        public BinanceCanceledOrder? CanceledOrder { get; set; }
        
    }
}
