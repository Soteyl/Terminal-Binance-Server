namespace Ixcent.CryptoTerminal.Domain.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            IEnumerable<T> forEach = source as T[] ?? source.ToArray();
            
            foreach (T? element in forEach)
            {
                action(element);
            }

            return forEach;
        }
    }
}