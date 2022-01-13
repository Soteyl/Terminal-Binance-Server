namespace Ixcent.CryptoTerminal.Domain.Interfaces
{
    public interface IAdvancedEnum
    {
        string Name { get; }

        byte Value { get; }

        SortedList<byte, IAdvancedEnum> Values { get; }
    }
}
