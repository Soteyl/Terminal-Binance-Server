using Binance.Net;
using CryptoTerminal.Models.CryptoExchanges;
using System.Threading;
using System;    
using Binance.Net.Interfaces;

namespace CryptoTerminal.Models.DemoExchanges
{
    public class DemoExchangeSimulation
    {

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
                var quotes = _client.Spot.Market.GetPricesAsync().Result.Data;
                foreach (var quote in quotes)
                {
                    foreach (var keyValuePair in _demoStorage.GetAllUserData())
                    {
                        string userKey = keyValuePair.Key;
                        DemoUserData userData = keyValuePair.Value;

                        var allMarketOrders = userData.OpenOrders.Where(ord => ord.OrderType == OrderType.Market && string.Equals(ord.Pair, quote.Symbol));
                        var allLimitOrders = userData.OpenOrders.Where(ord => ord.OrderType == OrderType.Limit && string.Equals(ord.Pair, quote.Symbol));

                        List<SpotOrder> fullfilledOrders = new List<SpotOrder>();

                        foreach (SpotOrder order in allMarketOrders)
                        {
                            bool success = _demoStorage.TryFullfillOrder(userKey, order);
                            if (success)
                                fullfilledOrders.Add(order);
                        }

                        foreach (SpotOrder order in allLimitOrders)
                        {
                            decimal quotePrice = quote.Price;
                            decimal limitPrice = order.Price;

                            if (order.OrderSide == OrderSide.Buy && limitPrice >= quotePrice ||
                                order.OrderSide == OrderSide.Sell && limitPrice <= quotePrice)
                            {
                                bool success = _demoStorage.TryFullfillOrder(userKey, order);
                                if (success)
                                    fullfilledOrders.Add(order);
                            }
                        }

                        foreach (var order in fullfilledOrders)
                            userData.OpenOrders.Remove(order);

                    }
                }
                Thread.Sleep(500);
            }
        }
    }
}
