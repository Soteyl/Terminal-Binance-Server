using Binance.Net.Objects.Futures.FuturesData;
using Binance.Net.Enums;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges
{
    using Data;
    using Results;

    public abstract class CryptoFutures
    {
        private readonly string _mainCoin;

        public CryptoFutures(string mainCoin)
        {
            _mainCoin = mainCoin;
        }

        public string MainCoin => _mainCoin;

        public abstract Task<IEnumerable<FuturesOrder>> GetOrdersHistory(); 

        public abstract Task<IEnumerable<FuturesTrade>> GetTradesHistory();

        public abstract Task<IEnumerable<FuturesOrder>> GetOpenOrders();

        public abstract Task<OrderBook> GetDepthOfMarket(string firstQuote);

        public abstract Task<CoinBalance> GetBalance();

        public abstract void CancelOrder(FuturesOrder order);

        public abstract Task<AdjustLeverageResult> AdjustLeverage(string symbol, int leverageValue);

        public abstract Task<ChangeMarginTypeResult> ChangeMarginType(string symbol, FuturesMarginType marginType);

        public abstract Task<MakeOrderResult> MakeOrder(FuturesOrder order);

    }
}