using Binance.Net;
using Binance.Net.Interfaces.SubClients.Spot;
using Binance.Net.Objects.Spot.SpotData;
using CryptoExchange.Net.Objects;

namespace CryptoTerminal.Models.CryptoExchanges.BinanceRealisation
{
    public class BinanceSpot: CryptoSpot
    {
        private IBinanceClientSpot _spot;

        internal BinanceSpot(IBinanceClientSpot spot)
        {
            _spot = spot;
        }

        public override void CancelOrder(SpotOrder order)
        {
            throw new NotImplementedException();
        }

        public override List<CoinBalance> GetCoinBalances()
        {
            throw new NotImplementedException();
        }

        public override List<SpotOrder> GetDepthOfMarket()
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<SpotOrder>> GetOpenOrders()
        {
            WebCallResult<IEnumerable<BinanceOrder>> res = await _spot.Order.GetOpenOrdersAsync();

            return res.Data.Select(a => new SpotOrder(a.Symbol, a.Quantity, a.Price, (OrderSide)(int)a.Side, (OrderType)(int)a.Type));
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
            throw new NotImplementedException();
            //WebCallResult<BinancePlacedOrder> callResult = await _spot.Order.PlaceOrderAsync(order.Pair,
            //                                                                                order.OrderSide.ConvertToBinanceOrderSide(),
            //                                                                                order.OrderType.ConvertToBinanceOrderType(),
            //                                                                                quantity: order.AmountFirst,
            //                                                                                price: order.Price,
            //                                                                                timeInForce: Binance.Net.Enums.TimeInForce.GoodTillCancel);


            //return new MakeOrderResult(callResult.Success, callResult.Error?.Message);
        }
    }
}
