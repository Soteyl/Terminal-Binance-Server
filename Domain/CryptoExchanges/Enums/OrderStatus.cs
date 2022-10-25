using Ixcent.CryptoTerminal.Domain.Common.Interfaces;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.Enums
{
    public sealed class OrderStatus : IAdvancedEnum
    {
        private OrderStatus(byte value, string name)
        {
            Value = value;
            Name = name;
            Values.Add(value, this);
        }

        public static SortedList<byte, IAdvancedEnum> Values { get; } = new();

        public static OrderStatus New { get; } = new(0, nameof(New));

        public static OrderStatus PartiallyFilled { get; } = new(1, nameof(PartiallyFilled));

        public static OrderStatus Filled { get; } = new(1, nameof(Filled));

        public static OrderStatus Canceled { get; } = new(1, nameof(Canceled));

        public static OrderStatus PendingCancel { get; } = new(1, nameof(PendingCancel));

        public static OrderStatus Rejected { get; } = new(1, nameof(Rejected));

        public static OrderStatus Expired { get; } = new(1, nameof(Expired));

        public static OrderStatus Insurance { get; } = new(1, nameof(Insurance));

        public static OrderStatus Adl { get; } = new(1, nameof(Adl));

        public string Name { get; }

        public byte Value { get; }

        public static implicit operator Binance.Net.Enums.OrderStatus(OrderStatus value)
        {
            return (Binance.Net.Enums.OrderStatus)value.Value;
        }

        public static implicit operator OrderStatus(Binance.Net.Enums.OrderStatus value)
        {
            return (OrderStatus)Values[(byte)value];
        }

        public static implicit operator CryptoExchange.Net.ExchangeInterfaces.IExchangeClient.OrderStatus(OrderStatus value)
        {
            if (value.Value != 0 && value != PartiallyFilled)
            {
                if (value != Filled)
                {
                    return CryptoExchange.Net.ExchangeInterfaces.IExchangeClient.OrderStatus.Canceled;
                }

                return CryptoExchange.Net.ExchangeInterfaces.IExchangeClient.OrderStatus.Filled;
            }

            return CryptoExchange.Net.ExchangeInterfaces.IExchangeClient.OrderStatus.Active;
        }

        public static implicit operator OrderStatus(CryptoExchange.Net.ExchangeInterfaces.IExchangeClient.OrderStatus value)
        {
            return (OrderStatus)Values[(byte)value];
        }

        SortedList<byte, IAdvancedEnum> IAdvancedEnum.Values => Values;
    }
}
