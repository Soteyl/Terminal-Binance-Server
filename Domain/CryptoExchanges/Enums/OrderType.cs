namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.Enums
{
    using Interfaces;
    using Ixcent.CryptoTerminal.Domain.Converters;
    using Newtonsoft.Json;

    public sealed class OrderType : IAdvancedEnum
    {
        private OrderType(byte value, string name)
        {
            Value = value;
            Name = name;
            Values.Add(value, this);
        }

        public static SortedList<byte, IAdvancedEnum> Values { get; } = new SortedList<byte, IAdvancedEnum>();

        public static OrderType Limit { get; } = new OrderType(0, nameof(Limit));

        public static OrderType Market { get; } = new OrderType(1, nameof(Market));

        public static OrderType StopLoss { get; } = new OrderType(2, nameof(StopLoss));

        public static OrderType StopLossLimit { get; } = new OrderType(3, nameof(StopLoss));

        public static OrderType Stop { get; } = new OrderType(4, nameof(Stop));

        public static OrderType StopMarket { get; } = new OrderType(5, nameof(StopMarket));

        public static OrderType TakeProfit { get; } = new OrderType(6, nameof(TakeProfit));

        public static OrderType TakeProfitMarket { get; } = new OrderType(7, nameof(TakeProfitMarket));

        public static OrderType TakeProfitLimit { get; } = new OrderType(8, nameof(TakeProfitLimit));

        public static OrderType LimitMaker { get; } = new OrderType(9, nameof(LimitMaker));

        public static OrderType TrailingStopMarket { get; } = new OrderType(10, nameof(TrailingStopMarket));

        public static OrderType Liquidation { get; } = new OrderType(11, nameof(Liquidation));

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

        public static implicit operator OrderType(string value)
        {
            return JsonConvert.DeserializeObject<OrderType>(value, new AdvancedEnumConverter());
        }

        public static implicit operator string(OrderType value)
        {
            return JsonConvert.SerializeObject(value, new AdvancedEnumConverter());
        }
        SortedList<byte, IAdvancedEnum> IAdvancedEnum.Values => Values;
    }
}
