using Ixcent.CryptoTerminal.Domain.CryptoExchanges.Enums;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.Data
{

    public class FuturesPosition
    {
        public FuturesPosition(string symbol, decimal quantity, decimal entryPrice, PositionSide side, decimal unrealizedPnl, int leverage, decimal markPrice)
        {
            Symbol = symbol;
            Quantity = quantity;
            EntryPrice = entryPrice;
            Side = side;
            UnrealizedPnl = unrealizedPnl;
            Leverage = leverage;
            MarkPrice = markPrice;
        }

        public string Symbol { get; }      

        public decimal EntryPrice { get; }

        public decimal Quantity { get; }

        public decimal MarkPrice { get; }

        public int Leverage { get; }

        public decimal UnrealizedPnl { get; }

        public PositionSide Side { get; }
    }
}