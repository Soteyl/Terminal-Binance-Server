namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.Enums
{
    using Interfaces;
    using Ixcent.CryptoTerminal.Domain.Converters;
    using Newtonsoft.Json;

    public sealed class TimeInForce : IAdvancedEnum
    {
        private TimeInForce(byte value, string name)
        {
            Value = value;
            Name = name;
            Values.Add(value, this);
        }

        public static SortedList<byte, IAdvancedEnum> Values { get; } = new SortedList<byte, IAdvancedEnum>();

        public static TimeInForce GoodTillCancel { get; } = new TimeInForce(0, nameof(GoodTillCancel));

        public static TimeInForce ImmediateOrCancel { get; } = new TimeInForce(1, nameof(ImmediateOrCancel));

        public static TimeInForce FillOrKill { get; } = new TimeInForce(2, nameof(FillOrKill));

        public static TimeInForce GoodTillCrossing { get; } = new TimeInForce(3, nameof(GoodTillCrossing));

        public static TimeInForce GoodTillExpiredOrCanceled { get; } = new TimeInForce(4, nameof(GoodTillExpiredOrCanceled));

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

        public static implicit operator TimeInForce(string value)
        {
            return JsonConvert.DeserializeObject<TimeInForce>(value, new AdvancedEnumConverter());
        }

        public static implicit operator string(TimeInForce value)
        {
            return JsonConvert.SerializeObject(value, new AdvancedEnumConverter());
        }

        SortedList<byte, IAdvancedEnum> IAdvancedEnum.Values => Values;
    }
}
