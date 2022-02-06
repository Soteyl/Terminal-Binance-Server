namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.Data
{
    using Binance.Net.Objects.Spot.SpotData;
    using CryptoExchange.Net.ExchangeInterfaces;
    using Enums;

    public class SpotOrder: ICommonOrder
    {
        public string Symbol { get; set; } = string.Empty;

        public decimal AmountFirst { get; set; }

        public decimal? Price { get; set; }

        public OrderSide OrderSide { get; set; }

        public OrderType OrderType { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public DateTime? DateTime { get; set; }

        public TimeInForce? TimeInForce { get; set; }

        public string Id { get; set; }

        public bool IsActive { get; set; }

        string ICommonOrder.CommonSymbol => Symbol;

        decimal ICommonOrder.CommonPrice => Price ?? 0;

        decimal ICommonOrder.CommonQuantity => AmountFirst;

        IExchangeClient.OrderStatus ICommonOrder.CommonStatus => OrderStatus;

        bool ICommonOrder.IsActive => IsActive;

        IExchangeClient.OrderSide ICommonOrder.CommonSide => OrderSide;

        IExchangeClient.OrderType ICommonOrder.CommonType => OrderType;

        DateTime ICommonOrder.CommonOrderTime => DateTime ?? new DateTime();

        string ICommonOrderId.CommonId => Id;

        public static implicit operator SpotOrder(BinanceOrder order)
        {
            return new SpotOrder
            {
                Symbol = order.Symbol,
                AmountFirst = order.Quantity,
                DateTime = order.CreateTime,
                OrderSide = order.Side,
                OrderType = order.Type,
                Price = order.Price,
                TimeInForce = order.TimeInForce,
                Id = order.ClientOrderId
            };
        }

        public static implicit operator BinanceOrder(SpotOrder order)
        {
            return order;
        }
    }
}
