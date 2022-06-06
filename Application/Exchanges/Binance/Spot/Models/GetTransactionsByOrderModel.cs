using System.ComponentModel.DataAnnotations;

using MediatR;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Models
{
    /// <summary>
    /// Model for <see cref="Handlers.GetTransactionsByOrderHandler"/>. Contains information about symbol and orderId.
    /// </summary>
    /// <remarks>
    /// Implements <see cref="IRequest{TResponse}"/> <br/>
    /// <c>TResponse</c> is <see cref="Results.GetTransactionsByOrderResult"/>
    /// </remarks>
    public class GetTransactionsByOrderModel: IRequest<Results.GetTransactionsByOrderResult>
    {
        /// <summary>
        /// Symbol of crypto pair like BTCUSDT
        /// </summary>
        [Required]
        public string Symbol { get; set; }

        /// <summary>
        /// Id of the binance spot market order
        /// </summary>
        [Required]
        public long? OrderId { get; set; }
    }
}
