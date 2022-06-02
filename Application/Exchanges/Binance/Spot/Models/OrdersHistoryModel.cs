using MediatR;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Models
{
    using Results;
    public class OrdersHistoryModel : IRequest<OrdersHistoryResult>
    {
    }
}
