using Binance.Net;
using Binance.Net.Interfaces.SubClients;
using Binance.Net.Interfaces.SubClients.Spot;
using Binance.Net.Objects.Spot.SpotData;
using CryptoExchange.Net.Objects;

namespace CryptoTerminal.Models.CryptoExchanges.BinanceRealisation
{
    public class BinanceSpot : CryptoSpot
    {
        private IBinanceClientSpot _spot;

        private IBinanceClientGeneral _general;

        internal BinanceSpot(IBinanceClientSpot spot, IBinanceClientGeneral general)
        {
            _spot = spot;
            _general = general;
        }

        public override void CancelOrder(SpotOrder order)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<CoinBalance>> GetCoinBalances()
        {
            WebCallResult<BinanceAccountInfo> info = await _general.GetAccountInfoAsync();

            IEnumerable<BinanceBalance> balances = info.Data.Balances;

            return balances.Select(balance => new CoinBalance(balance.Asset, balance.Free, balance.Locked));
        }

        public override IEnumerable<BookPrice> GetCoinPairs()
        {
            var result = _spot.Market.GetAllBookPricesAsync();
            result.Wait();
            
            return result.Result.Data.Select(a => a.ToIxcentBookPrice());
        }

        public override List<SpotOrder> GetDepthOfMarket()
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<SpotOrder>> GetOpenOrders()
        {
            WebCallResult<IEnumerable<BinanceOrder>> res = await _spot.Order.GetOpenOrdersAsync();

            return res.Data.Select(a => new SpotOrder(a.Symbol, a.Quantity, (OrderSide)(int)a.Side, (OrderType)(int)a.Type, price: a.Price));
        }

        public override List<SpotOrder> GetOrderHistory()
        {
            throw new NotImplementedException();
        }

        public override List<Transaction> GetTransactionsHistory()
        {
            throw new NotImplementedException();
        }

        public override async Task<MakeOrderResult> MakeOrder(SpotOrder order)
        {
            WebCallResult<BinancePlacedOrder> callResult = 
              await _spot.Order.PlaceOrderAsync(order.Pair,
                                                order.OrderSide.ConvertToBinanceOrderSide(),
                                                order.OrderType.ConvertToBinanceOrderType(),
                                                quantity: order.AmountFirst,
                                                price: order.Price,
                                                timeInForce: order.TimeInForce?.ToBinanceTIF());


            return new MakeOrderResult(callResult.Success, callResult.Error?.Message);
        }
    }
}
