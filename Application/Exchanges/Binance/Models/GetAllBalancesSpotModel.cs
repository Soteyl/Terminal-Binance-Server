using Binance.Net.Objects.Spot.SpotData;
using MediatR;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Models
{
    public class GetAllBalancesSpotModel : IRequest<IEnumerable<BinanceBalance>>
    {

    }
}
