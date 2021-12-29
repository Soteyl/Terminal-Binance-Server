using CryptoExchange.Net.Attributes;

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

        public abstract Task<IEnumerable<FuturesOrder>> GetOrdersHistory();

        public abstract Task<IEnumerable<FuturesOrder>> GetOpenOrders();

        public abstract Task<OrderBook> GetDepthOfMarket(string firstQuote);

        public abstract Task<CoinBalance> GetBalance();

        public abstract void CancelOrder(FuturesOrder order);

        public abstract void AdjustLeverage(int value);

        public abstract void ChangeMarginType();

        public abstract Task<MakeOrderResult> MakeOrder(FuturesOrder order);

    }
}