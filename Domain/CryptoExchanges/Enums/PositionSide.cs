﻿using Ixcent.CryptoTerminal.Domain.Common.Interfaces;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.Enums
{
    public sealed class PositionSide : IAdvancedEnum
    {
        private PositionSide(byte value, string name)
        {
            Value = value;
            Name = name;
            Values.Add(value, this);
        }

        public static SortedList<byte, IAdvancedEnum> Values { get; } = new();

        public static PositionSide Short { get; } = new(0, nameof(Short));

        public static PositionSide Long { get; } = new(1, nameof(Long));

        public static PositionSide Both { get; } = new(1, nameof(Both));

        public string Name { get; }

        public byte Value { get; }

        public static implicit operator Binance.Net.Enums.PositionSide(PositionSide value)
        {
            return (Binance.Net.Enums.PositionSide)value.Value;
        }

        public static implicit operator PositionSide(Binance.Net.Enums.PositionSide value)
        {
            return (PositionSide)Values[(byte)value];
        }

        SortedList<byte, IAdvancedEnum> IAdvancedEnum.Values => Values;
    }
}
