﻿using Binance.Net;
using Binance.Net.Interfaces.SubClients.Spot;
using Binance.Net.Objects.Spot.SpotData;
using CryptoExchange.Net.Objects;

namespace CryptoTerminal.Models.CryptoExchanges.BinanceRealisation
{
    public class BinanceSpot : CryptoSpot
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
