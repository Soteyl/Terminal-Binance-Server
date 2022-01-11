using Binance.Net;
using CryptoExchange.Net.ExchangeInterfaces;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.DemoExchanges
{
    using Data;
    using Enums;

    public class DemoExchangeSimulation
    {
        private const int _updateTime = 100;
        private IAccessDemoStorage _demoStorage;
        private BinanceClient _client;
        private bool _isRunningSimulation = false;
        private Thread _thread;

        public DemoExchangeSimulation(IAccessDemoStorage demoStorage)
        {
            _demoStorage = demoStorage;
            _client = new BinanceClient();
        }

        public void RunSimulation()
        {
            if (_isRunningSimulation)
                return;

            _isRunningSimulation = true;

            _thread = new Thread(Update);
            _thread.Start();
        }

        public void StopSimulation()
        {
            _isRunningSimulation = false;
        }

        public void Update()
        {
            while (_isRunningSimulation)
            {
                foreach (var keyValuePair in _demoStorage.GetAllUserData())
                {
                    string userKey = keyValuePair.Key;
                    DemoUserData userData = keyValuePair.Value;

                    var allMarketOrders = userData.OpenOrders.Where(ord => ord.CommonType == IExchangeClient.OrderType.Market);
                    var allLimitOrders = userData.OpenOrders.Where(ord => ord.CommonType == IExchangeClient.OrderType.Limit);

                    List<ICommonOrder> fullfilledOrders = new List<ICommonOrder>();

                    foreach (ICommonOrder order in allMarketOrders)
                    {
                        var quote = _client.Spot.Market.GetPriceAsync(symbol: order.CommonSymbol).Result.Data;

                        bool success = _demoStorage.TryFullfillMarketOrder(userKey, quote.Price, order);
                        if (success)
                            fullfilledOrders.Add(order);
                    }

                    foreach (ICommonOrder order in allLimitOrders)
                    {
                        var quote = _client.Spot.Market.GetPriceAsync(symbol: order.CommonSymbol).Result.Data;
                        decimal quotePrice = quote.Price;

                        bool success = _demoStorage.TryFullfillLimitOrder(userKey, quote.Price, order);
                        if (success)
                            fullfilledOrders.Add(order);
                    }

                    _demoStorage.RemoveUserOrders(userKey, fullfilledOrders.ToArray());

                }
                Thread.Sleep(_updateTime);
            }
        }
    }
}
