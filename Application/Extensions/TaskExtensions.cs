namespace Ixcent.CryptoTerminal.Application
{
    public static class TaskExtensions
    {
        /// <summary>
        /// Waits task to end and returns completed task
        /// </summary>
        /// <returns>Completed task</returns>
        public static Task WaitThis(this Task source)
        {
            source.Wait();
            return source;
        }

        /// <summary>
        /// Waits task to end and returns completed task
        /// </summary>
        /// <returns>Completed task</returns>
        public static Task<T> WaitThis<T>(this Task<T> source)
        {
            source.Wait();
            return source;
        }
    }
}
