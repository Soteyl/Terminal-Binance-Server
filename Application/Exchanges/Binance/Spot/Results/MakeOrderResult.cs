using Binance.Net.Objects.Spot.SpotData;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Results
{
    /// <summary>
    /// Result of making Binance spot orders
    /// </summary>
    public class MakeOrderResult
    {

        public BinancePlacedOrder PlacedOrder { get; set; }

    }
}
