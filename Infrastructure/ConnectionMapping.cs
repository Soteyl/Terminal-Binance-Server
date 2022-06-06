namespace Ixcent.CryptoTerminal.Infrastructure
{
    /// <summary>
    /// Special collection for SignalR Hub connections.
    /// </summary>
    /// <typeparam name="T">type of key</typeparam>
    public class ConnectionMapping<T>
        where T : notnull
    {
        private readonly Dictionary<T, HashSet<string>> _connections =
            new Dictionary<T, HashSet<string>>();

        /// <summary>
        /// Invokes when all connections from a key are removed.
        /// </summary>
        public event Action<T> OnKeyEmpty;

        /// <summary>
        /// A count of connection keys.
        /// </summary>
        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public void Add(T key, string connectionId)
        {
            lock (_connections)
            {
                if (!_connections.TryGetValue(key, out HashSet<string> connections))
                {
                    connections = new HashSet<string>();
                    _connections.Add(key, connections);
                }

                lock (connections)
                {
                    connections.Add(connectionId);
                }
            }
        }

        public IEnumerable<string> GetConnections(T key)
        {
            if (_connections.TryGetValue(key, out HashSet<string> connections))
            {
                return connections;
            }

            return Enumerable.Empty<string>();
        }

        public void Remove(T key, string connectionId)
        {
            lock (_connections)
            {
                if (!_connections.TryGetValue(key, out HashSet<string> connections))
                {
                    return;
                }

                lock (connections)
                {
                    connections.Remove(connectionId);

                    if (connections.Count == 0)
                    {
                        _connections.Remove(key);
                        OnKeyEmpty?.Invoke(key);
                    }
                }
            }
        }

        /// <summary>
        /// Removes all connections from all keys 
        /// </summary>
        /// <param name="connectionId"></param>
        public void RemoveByConnectionId(string connectionId)
        {
            lock (_connections)
            {
                foreach (KeyValuePair<T, HashSet<string>> connection in _connections)
                {
                    Remove(connection.Key, connectionId);
                }
            }
            //lock (_connections)
            //{
            //    HashSet<T> keysToRemove = new HashSet<T>();
            //    foreach (KeyValuePair<T, HashSet<string>> connection in _connections)
            //    {
            //        if (connection.Value.Contains(connectionId))
            //        {
            //            connection.Value.Remove(connectionId);
            //            if (connection.Value.Count == 0)
            //            {
            //                keysToRemove.Add(connection.Key);
            //            }
            //        }
            //    }
            //    foreach (T? key in keysToRemove)
            //    {
            //        _connections.Remove(key);
            //    }

        }
    }
}