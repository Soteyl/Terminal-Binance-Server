using MediatR;
using Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Results;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Models
{
    public class OrdersHistoryModel : IRequest<OrdersHistoryResult>
    {
        public string Symbol { get; set; } = string.Empty;
    }
}
