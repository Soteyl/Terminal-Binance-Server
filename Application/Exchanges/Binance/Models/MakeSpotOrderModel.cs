using Binance.Net.Enums;
using MediatR;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Models
{
    /// <summary>
    /// Request model for making Binance spot orders. <para/>
    /// 
    /// Implements <see cref="IRequest{TResponse}"/> whose generic type is <see cref="MakeSpotOrderResult"/>
    /// </summary>
    public class MakeSpotOrderModel : IRequest<MakeSpotOrderResult>
    {
        public string Symbol { get; set; } = string.Empty;

        public string Id { get; set; } = string.Empty;

        public decimal Quantity { get; set; }

        public decimal? Price { get; set; }

        public OrderSide OrderSide { get; set; }

        public OrderType OrderType { get; set; }

        public DateTime? DateTime { get; set; }

        public TimeInForce? TimeInForce { get; set; }
    }
}
