using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixcent.CryptoTerminal.Application
{
    public static class TaskExtensions
    {
        public static Task WaitThis(this Task source)
        {
            source.Wait();
            return source;
        }

        public static Task<T> WaitThis<T>(this Task<T> source)
        {
            source.Wait();
            return source;
        }
    }
}
