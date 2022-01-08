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

        public abstract Task<IEnumerable<BinanceFuturesUsdtTrade>> GetOrdersHistory(); // TODO заменить класс на собственный Trade

        public abstract Task<IEnumerable<BinanceFuturesUsdtTrade>> GetTradesHistory();

        public abstract Task<IEnumerable<FuturesOrder>> GetOpenOrders();

        public abstract Task<OrderBook> GetDepthOfMarket(string firstQuote);

        public abstract Task<CoinBalance> GetBalance();

        public abstract void CancelOrder(FuturesOrder order);

        public abstract Task<BinanceFuturesInitialLeverageChangeResult> AdjustLeverage(string symbol, int leverageValue);

        public abstract Task<BinanceFuturesChangeMarginTypeResult> ChangeMarginType(string symbol, FuturesMarginType marginType);

        public abstract Task<MakeOrderResult> MakeOrder(FuturesOrder order);

        public abstract Task<IEnumerable<FuturesPosition>> GetAllPositions();

        public async void CloseAllPositions()
        {
            var allPositions = await GetAllPositions();

            foreach (var position in allPositions)
            {
                await ClosePosition(position);
            }
        }

        public async Task<MakeOrderResult> ClosePosition(FuturesPosition position)
        {
            return await MakeOrder(new FuturesOrder(
                symbol: position.Symbol, 
                amount: position.Quantity, 
                orderSide: OrderSide.Buy, 
                orderType: Enums.OrderType.Market, 
                positionSide: position.Side, 
                date: DateTime.Now, 
                0, 
                price: position.MarkPrice, 
                tif: TimeInForce.GoodTillExpiredOrCanceled, 
                reduceOnly: true)
            );
        }

    }
}