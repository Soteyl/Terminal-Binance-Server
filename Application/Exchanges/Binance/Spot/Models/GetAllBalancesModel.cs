using MediatR;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Models
{
    /// <summary>
    /// Model for <see cref="Handlers.GetAllBalancesHandler"/><br/>
    /// Used by: <c> MediatR </c>
    /// </summary>
    public class GetAllBalancesModel : IRequest<Results.GetAllBalancesResult>
    {
    }
}
