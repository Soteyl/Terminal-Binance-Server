using Ixcent.CryptoTerminal.Domain.CryptoExchanges.Enums;
using Binance.Net.Objects.Futures.FuturesData;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.Data
{

    public class FuturesPosition
    {
        public string Symbol { get; set; } = string.Empty;

        public decimal EntryPrice { get; set; }

        public decimal Quantity { get; set; }

        public int Leverage { get; set; }

        public decimal UnrealizedPnl { get; set; }

        public int OrderId { get; set; }

        public PositionSide Side { get; set; }

        public static implicit operator FuturesPosition(BinancePositionInfoUsdt pos)
        {
            return new FuturesPosition
            {
                Symbol = pos.Symbol,
                EntryPrice = pos.EntryPrice,
                Quantity = pos.Quantity,
                Leverage = pos.Leverage,
                UnrealizedPnl = pos.UnrealizedPnl,
                Side = pos.PositionSide
            };

        }

        public static implicit operator BinancePositionInfoUsdt(FuturesPosition pos)
        {
            return pos;
        }
    }
}