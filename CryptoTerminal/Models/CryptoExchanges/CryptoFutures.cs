using Binance.Net.Objects.Futures.FuturesData;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.ExchangeInterfaces;

namespace CryptoTerminal.Models.CryptoExchanges
{
    public abstract class CryptoFutures
    {
        private readonly string _mainCoin;

        public CryptoFutures(string mainCoin)
        {
            _mainCoin = mainCoin;
        }

        public string MainCoin => _mainCoin;

        public abstract Task<IEnumerable<BinanceFuturesUsdtTrade>> GetOrdersHistory();

        public abstract Task<IEnumerable<FuturesOrder>> GetOpenOrders();

        public abstract Task<OrderBook> GetDepthOfMarket(string firstQuote);

        public abstract Task<CoinBalance> GetBalance();

        public abstract void CancelOrder(FuturesOrder order);

        public abstract AdjustLeverageResult AdjustLeverage(string symbol, int leverageValue);

        public abstract void ChangeMarginType();

        public abstract Task<MakeOrderResult> MakeOrder(FuturesOrder order);

    }
}