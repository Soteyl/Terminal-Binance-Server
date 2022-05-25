using System.Threading.Tasks;
using System.Threading;
using Binance.Net;
using MediatR;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Handlers
{
    using Models;
    using Results;

    public class GetAllPricesHandler : IRequestHandler<SymbolPricesModel, SymbolPricesResult>
    {
        public async Task<SymbolPricesResult> Handle(SymbolPricesModel request, CancellationToken cancellationToken)
        {
            return new SymbolPricesResult
            {
                Prices = (await new BinanceClient().Spot.Market.GetPricesAsync()).Data
            };
        }
    }
}
