using Ixcent.CryptoTerminal.Domain.Interfaces;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.Enums
{
    public sealed class OrderType : IAdvancedEnum
    {
        private OrderType(byte value, string name)
        {
            Value = value;
            Name = name;
            Values.Add(value, this);
        }

        public static SortedList<byte, IAdvancedEnum> Values { get; } = new();

        public static OrderType Limit { get; } = new(0, nameof(Limit));

        public static OrderType Market { get; } = new(1, nameof(Market));

        public static OrderType StopLoss { get; } = new(2, nameof(StopLoss));

        public static OrderType StopLossLimit { get; } = new(3, nameof(StopLoss));

        public static OrderType Stop { get; } = new(4, nameof(Stop));

        public static OrderType StopMarket { get; } = new(5, nameof(StopMarket));

        public static OrderType TakeProfit { get; } = new(6, nameof(TakeProfit));

        public static OrderType TakeProfitMarket { get; } = new(7, nameof(TakeProfitMarket));

        public static OrderType TakeProfitLimit { get; } = new(8, nameof(TakeProfitLimit));

        public static OrderType LimitMaker { get; } = new(8, nameof(LimitMaker));

        public static OrderType TrailingStopMarket { get; } = new(9, nameof(TrailingStopMarket));

        public static OrderType Liquidation { get; } = new(10, nameof(Liquidation));

        public string Name { get; }

        public byte Value { get; }

        public static implicit operator Binance.Net.Enums.OrderType(OrderType value)
        {
            return (Binance.Net.Enums.OrderType)value.Value;
        }

        public static implicit operator OrderType(Binance.Net.Enums.OrderType value)
        {
            return (OrderType)Values[(byte)value];
        }

        public static implicit operator CryptoExchange.Net.ExchangeInterfaces.IExchangeClient.OrderType(OrderType value)
        {
            if (value == Limit)
            {
                return CryptoExchange.Net.ExchangeInterfaces.IExchangeClient.OrderType.Limit;
            }

            if (value == Market)
            {
                return CryptoExchange.Net.ExchangeInterfaces.IExchangeClient.OrderType.Market;
            }

            return CryptoExchange.Net.ExchangeInterfaces.IExchangeClient.OrderType.Other;
        }

        public static implicit operator OrderType(CryptoExchange.Net.ExchangeInterfaces.IExchangeClient.OrderType value)
        {
            return (OrderType)Values[(byte)value];
        }

        SortedList<byte, IAdvancedEnum> IAdvancedEnum.Values => Values;
    }
}
