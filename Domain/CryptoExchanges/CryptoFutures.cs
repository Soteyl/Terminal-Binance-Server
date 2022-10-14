using Binance.Net.Enums;

using Ixcent.CryptoTerminal.Domain.CryptoExchanges.Data;
using Ixcent.CryptoTerminal.Domain.CryptoExchanges.Results;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges
{
    public abstract class CryptoFutures
    {
        private readonly string _mainCoin;

        protected CryptoFutures(string mainCoin)
        {
            _mainCoin = mainCoin;
        }

        public string MainCoin => _mainCoin;

        public abstract Task<IEnumerable<FuturesOrder>> GetOrdersHistory();

        public abstract Task<IEnumerable<FuturesTrade>> GetTradesHistory();

        public abstract Task<IEnumerable<FuturesOrder>> GetOpenOrders();

        public abstract Task<OrderBook> GetDepthOfMarket(string firstQuote);

        public abstract Task<IEnumerable<FuturesBalance>> GetBalances();

        public abstract void CancelOrder(FuturesOrder order);

        public abstract Task<AdjustLeverageResult> AdjustLeverage(string symbol, int leverageValue);

        public abstract Task<ChangeMarginTypeResult> ChangeMarginType(string symbol, FuturesMarginType marginType);

        public abstract Task<MakeOrderResult> MakeOrder(FuturesOrder order);

        public abstract Task<IEnumerable<FuturesPosition>> GetAllPositions();

        public async void CloseAllPositions()
        {
            IEnumerable<FuturesPosition>? allPositions = await GetAllPositions();

            foreach (FuturesPosition? position in allPositions)
            {
                await ClosePosition(position);
            }
        }

        public abstract Task<MakeOrderResult> ClosePosition(FuturesPosition position);
    }
}