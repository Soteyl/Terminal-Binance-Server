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

        public abstract Task<FuturesOrder> GetOpenOrders();

        public abstract Task<OrderBook> GetDepthOfMarket();

        public abstract Task<CoinBalance> GetUSDTBalance();

        public abstract void AdjustLeverage(int value);

        public abstract void ChangeMarginType();

        public Task<CoinBalance> GetUSDTFreeBalance()
        {
            throw new NotImplementedException();
        }

        public abstract Task<MakeOrderResult> MakeOrder(FuturesOrder order);

    }
}