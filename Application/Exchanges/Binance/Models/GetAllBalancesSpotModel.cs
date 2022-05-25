using Binance.Net.Objects.Spot.SpotData;
using MediatR;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Models
{
    using Binance.Handlers;
    /// <summary>
    /// Model for <see cref="GetAllBalancesSpotHandler"/><br/>
    /// Used by: <c> MediatR </c>
    /// </summary>
    public class GetAllBalancesSpotModel : IRequest<GetAllBalancesSpotResult>
    {
    }
}
