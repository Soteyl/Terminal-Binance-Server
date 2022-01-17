namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.Enums
{
    using Interfaces;
    using Ixcent.CryptoTerminal.Domain.Converters;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public sealed class OrderSide : IAdvancedEnum
    {
        private OrderSide(byte value, string name)
        {
            Value = value;
            Name = name;
            Values.Add(value, this);
        }

        public static SortedList<byte, IAdvancedEnum> Values { get; } = new SortedList<byte, IAdvancedEnum>();

        public static OrderSide Buy { get; } = new OrderSide(0, nameof(Buy));

        public static OrderSide Sell { get; } = new OrderSide(1, nameof(Sell));

        public string Name { get; }

        public byte Value { get; }

        public static implicit operator Binance.Net.Enums.OrderSide(OrderSide value)
        {
            return (Binance.Net.Enums.OrderSide)value.Value;
        }

        public static implicit operator OrderSide(Binance.Net.Enums.OrderSide value)
        {
            return (OrderSide)Values[(byte)value];
        }

        public static implicit operator OrderSide(string value)
        {
            return JsonConvert.DeserializeObject<OrderSide>(value, new AdvancedEnumConverter());
        }

        public static implicit operator string(OrderSide value)
        {
            return JsonConvert.SerializeObject(value, new AdvancedEnumConverter());
        }
        SortedList<byte, IAdvancedEnum> IAdvancedEnum.Values => Values;
    }
}
