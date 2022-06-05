using Binance.Net.Objects.Futures.FuturesData;

using Ixcent.CryptoTerminal.Domain.CryptoExchanges.Enums;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.Data
{
    public class FuturesOrder
    {
        private decimal? _price;

        private TimeInForce? _tif;

        public long Id { get; set; }

        public string Symbol { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public OrderType OrderType { get; set; }

        public OrderSide OrderSide { get; set; }

        public DateTime CreatedDate { get; set; }

        public decimal? Price { get => (OrderType == OrderType.Limit) ? _price : null; set => _price = value; }

        public TimeInForce? Tif { get => (OrderType == OrderType.Limit) ? _tif : null; set => _tif = value; }

        public PositionSide PositionSide { get; set; }

        public bool ClosePosition { get; set; }

        public bool ReduceOnly { get; set; }

        public static implicit operator FuturesOrder(BinanceFuturesOrder order)
        {
            return new FuturesOrder
            {
                Symbol = order.Symbol,
                Amount = order.Quantity,
                OrderSide = order.Side,
                OrderType = order.Type,
                PositionSide = order.PositionSide,
                CreatedDate = order.CreatedTime,
                Id = order.OrderId,
                Price = order.Price,
                Tif = order.TimeInForce,
                ReduceOnly = order.ReduceOnly
            };
        }

        public static implicit operator BinanceFuturesOrder(FuturesOrder order)
        {
            return order;
        }
    }
}
