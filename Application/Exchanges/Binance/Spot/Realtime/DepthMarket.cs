using Binance.Net;
using Binance.Net.Interfaces;
using Binance.Net.Objects;

using CryptoExchange.Net.Sockets;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Realtime
{
    /// <summary>
    /// Subscribes to a Binance spot market, checks updated in a market depth for a coin.
    /// </summary>
    /// <remarks> Implements <see cref="IDisposable"/></remarks>
    public class DepthMarket
        : IDisposable
    {
        private readonly BinanceSocketClient _binanceClient = new BinanceSocketClient(new BinanceSocketClientOptions()
        {
            AutoReconnect = true,
            ReconnectInterval = TimeSpan.FromSeconds(15)
        });

        private readonly Dictionary<string, UpdateSubscription> _subscriptions
            = new Dictionary<string, UpdateSubscription>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Subscribes to a symbol. <br/>
        /// After subscribing <see cref="DepthMarketUpdated"/> event starting return information about this symbol.
        /// </summary>
        public void SubscribeTo(string symbol)
        {
            lock (_subscriptions)
            {
                if (_subscriptions.ContainsKey(symbol)) return;

                var callResult = _binanceClient.Spot.SubscribeToOrderBookUpdatesAsync(symbol, 1000, ReceiveDepthMarketUpdate);
                callResult.Wait();
                if (callResult.Result.Success == false) return;

                _subscriptions.Add(symbol, callResult.Result.Data);
            }
        }

        /// <summary> Unsubscribes from a symbol </summary>
        public void UnsubscribeFrom(string symbol)
        {
            lock (_subscriptions)
            {
                if (_subscriptions.ContainsKey(symbol) == false) return;

                _subscriptions[symbol].CloseAsync().Wait();

                _subscriptions.Remove(symbol);
            }
        }

        /// <summary> Disposes Binance client </summary>
        public void Dispose()
        {
            _binanceClient.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary> Return updated information about subscribed symbols </summary>
        public event EventHandler<IBinanceOrderBook> DepthMarketUpdated;

        private void ReceiveDepthMarketUpdate(DataEvent<IBinanceEventOrderBook> @event)
        {
            DepthMarketUpdated?.Invoke(this, @event.Data);
        }
    }
}
