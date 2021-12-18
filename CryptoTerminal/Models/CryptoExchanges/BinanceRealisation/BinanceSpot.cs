using Binance.Net;
using Binance.Net.Interfaces.SubClients.Spot;
using Binance.Net.Objects.Spot.SpotData;
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Objects;

namespace CryptoTerminal.Models.CryptoExchanges.BinanceRealisation
{
    public class BinanceSpot: CryptoSpot
    {
        private IBinanceClientSpot _spot;

        private IExchangeClient _exchangeClient;

        internal BinanceSpot(IBinanceClientSpot spot, IExchangeClient exClient)
        {
            _spot = spot;
            _exchangeClient = exClient;
        }

        public override void CancelOrder(SpotOrder order)
        {
            throw new NotImplementedException();
        }

        public override List<CoinBalance> GetCoinBalances()
        {
            throw new NotImplementedException();
        }

        public override async Task<OrderBook> GetDepthOfMarket(string symbol)
        {
            WebCallResult<ICommonOrderBook> res = await _exchangeClient.GetOrderBookAsync(symbol);
            var preparedResult = new OrderBook(res.Data.CommonBids.Select(bid => new OrderBookEntry(bid.Quantity, bid.Price)),
                                                res.Data.CommonAsks.Select(bid => new OrderBookEntry(bid.Quantity, bid.Price)));
            return preparedResult;
        }

        public override List<SpotOrder> GetOpenOrders()
        {
            throw new NotImplementedException();
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
