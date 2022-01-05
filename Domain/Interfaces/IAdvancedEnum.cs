using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixcent.CryptoTerminal.Domain.Interfaces
{
    public interface IAdvancedEnum
    {
        string Name { get; }

        byte Value { get; }

        SortedList<byte, IAdvancedEnum> Values { get; }
    }
}
