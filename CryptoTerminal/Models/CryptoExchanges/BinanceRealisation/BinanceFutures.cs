using Binance.Net.Interfaces;
using Binance.Net.Interfaces.SubClients.Futures;
using Binance.Net.Objects;
using Binance.Net.Objects.Futures.FuturesData;
using Binance.Net.Objects.Futures;  
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;

namespace CryptoTerminal.Models.CryptoExchanges.BinanceRealisation
{
    public class BinanceFutures : CryptoFutures
    {
        private IBinanceClient _client;
        private IExchangeClient _exClient;
        public BinanceFutures(IBinanceClient binanceClient, IExchangeClient exClient, string mainCoin) 
            : base(mainCoin)
        {
            _exClient = exClient;
            _client = binanceClient;
        }

        public override void AdjustLeverage(int value)
        {
            throw new NotImplementedException();
        }

        public override void ChangeMarginType()
        {
            throw new NotImplementedException();
        }

        public override Task<OrderBook> GetDepthOfMarket()
        {
            throw new NotImplementedException();
        }

        public override Task<FuturesOrder> GetOpenOrders()
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<ICommonOrder>> GetOrdersHistory()
        {
            IEnumerable<ICommonSymbol> symbols = (await _exClient.GetSymbolsAsync()).Data;

            List<ICommonOrder> ordersHistory = new List<ICommonOrder>();

            foreach (var symbol in symbols.Where(s => s.CommonName.Contains(MainCoin)))
            {
                IEnumerable<ICommonOrder> closedOrdersResult = (await _exClient.GetClosedOrdersAsync(symbol.CommonName)).Data;
                ordersHistory.AddRange(closedOrdersResult);
            }

            return ordersHistory;
        }

        public override Task<CoinBalance> GetUSDTBalance()
        {
            throw new NotImplementedException();
        }

        public override async Task<MakeOrderResult> MakeOrder(FuturesOrder order)
        {
            throw new NotImplementedException();
        }
        
    }
}
