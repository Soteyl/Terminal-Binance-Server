using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Objects;
using Binance.Net.Interfaces;
using Binance.Net;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance
{
    /// <summary>
    /// Subscribes to a Binance spot market, checks updated in a market depth for a coin.
    /// </summary>
    public class RealtimeSpotDepthMarket
    {
        private readonly BinanceSocketClient _binanceClient = new BinanceSocketClient();

        private readonly Dictionary<string, UpdateSubscription> _subscriptions = new Dictionary<string,UpdateSubscription>();

        /// <summary>
        /// Subscribes to a symbol. <br/>
        /// After subscribing <see cref="DepthMarketUpdated"/> event starting return information about this symbol.
        /// </summary>
        public async void SubscribeTo(string symbol)
        {
            if (_subscriptions.ContainsKey(symbol)) return;

            CallResult<UpdateSubscription>? callResult = 
                await _binanceClient.Spot.SubscribeToOrderBookUpdatesAsync(symbol, 1000, ReceiveDepthMarketUpdate);

            if (callResult.Success == false) return;

            _subscriptions.Add(symbol, callResult.Data);
        }

        /// <summary>
        /// Unsubscribes from a symbol
        /// </summary>
        public async void UnsubscribeFrom(string symbol)
        {
            if (_subscriptions.ContainsKey(symbol) == false) return;

            await _subscriptions[symbol].CloseAsync();

            _subscriptions.Remove(symbol);
        }

        /// <summary>
        /// Return updated information about subscribed symbols
        /// </summary>
        public event EventHandler<IBinanceOrderBook> DepthMarketUpdated;

        private void ReceiveDepthMarketUpdate(DataEvent<IBinanceEventOrderBook> @event)
        {
            DepthMarketUpdated.Invoke(this, @event.Data);
        }
    }
}
