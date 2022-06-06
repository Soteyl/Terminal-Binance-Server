using System.ComponentModel.DataAnnotations;
using MediatR;
using Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Results;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Models
{
    /// <summary>
    /// Model for canceling binance spot order.
    /// </summary>
    public class CancelOpenOrderModel : IRequest<CancelOpenOrderResult>
    {
        /// <summary>
        /// Symbol of the order to cancel.
        /// </summary>
        [Required]
        public string? Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Id of the order to cancel.
        /// </summary>
        [Required]
        public long? Id { get; set; }
    }
}
