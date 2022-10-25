using Ixcent.CryptoTerminal.Domain.Common.Interfaces;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.Enums
{
    public sealed class TimeInForce : IAdvancedEnum
    {
        private TimeInForce(byte value, string name)
        {
            Value = value;
            Name = name;
            Values.Add(value, this);
        }

        public static SortedList<byte, IAdvancedEnum> Values { get; } = new();

        public static TimeInForce GoodTillCancel { get; } = new(0, nameof(GoodTillCancel));

        public static TimeInForce ImmediateOrCancel { get; } = new(1, nameof(ImmediateOrCancel));

        public static TimeInForce FillOrKill { get; } = new(2, nameof(FillOrKill));

        public static TimeInForce GoodTillCrossing { get; } = new(3, nameof(GoodTillCrossing));

        public static TimeInForce GoodTillExpiredOrCanceled { get; } = new(4, nameof(GoodTillExpiredOrCanceled));

        public string Name { get; }

        public byte Value { get; }

        public static implicit operator Binance.Net.Enums.TimeInForce(TimeInForce value)
        {
            return (Binance.Net.Enums.TimeInForce)value.Value;
        }

        public static implicit operator TimeInForce(Binance.Net.Enums.TimeInForce value)
        {
            return (TimeInForce)Values[(byte)value];
        }

        SortedList<byte, IAdvancedEnum> IAdvancedEnum.Values => Values;
    }
}
