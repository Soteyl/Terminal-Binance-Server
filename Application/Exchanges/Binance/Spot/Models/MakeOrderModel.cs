using Binance.Net.Enums;
using MediatR;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Models
{
    using Results;

    /// <summary>
    /// Request model for making Binance spot orders. <para/>
    /// 
    /// Implements <see cref="IRequest{TResponse}"/> whose generic type is <see cref="MakeOrderResult"/>
    /// </summary>
    public class MakeOrderModel : IRequest<MakeOrderResult>
    {
        public string Symbol { get; set; } = string.Empty;

        public string Id { get; set; } = string.Empty;

        public decimal Quantity { get; set; }

        public decimal? Price { get; set; }

        public decimal? IcebergQuantity { get; set; }

        public decimal? StopPrice { get; set; }

        public OrderSide OrderSide { get; set; }

        public OrderType OrderType { get; set; }

        public DateTime? DateTime { get; set; }

        public TimeInForce? TimeInForce { get; set; }
    }
}
