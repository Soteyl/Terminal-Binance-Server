using Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Results;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Models
{
    public class CancelOpenOrderModel : IRequest<CancelOpenOrderResult>
    {
        [Required]
        public string? Symbol { get; set; } = string.Empty;

        [Required]
        public long? Id { get; set; }
    }
}
