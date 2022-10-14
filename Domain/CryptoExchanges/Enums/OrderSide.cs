using Ixcent.CryptoTerminal.Domain.Interfaces;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.Enums
{
    public sealed class OrderSide : IAdvancedEnum
    {
        private OrderSide(byte value, string name)
        {
            Value = value;
            Name = name;
            Values.Add(value, this);
        }

        public static SortedList<byte, IAdvancedEnum> Values { get; } = new();

        public static OrderSide Buy { get; } = new(0, nameof(Buy));

        public static OrderSide Sell { get; } = new(1, nameof(Sell));

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

        public static implicit operator CryptoExchange.Net.ExchangeInterfaces.IExchangeClient.OrderSide(OrderSide value)
        {
            return (CryptoExchange.Net.ExchangeInterfaces.IExchangeClient.OrderSide)value.Value;
        }

        public static implicit operator OrderSide(CryptoExchange.Net.ExchangeInterfaces.IExchangeClient.OrderSide value)
        {
            return (OrderSide)Values[(byte)value];
        }

        SortedList<byte, IAdvancedEnum> IAdvancedEnum.Values => Values;
    }
}
