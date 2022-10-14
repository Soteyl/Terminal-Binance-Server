using Binance.Net;
using Binance.Net.Objects.Spot.MarketData;

using Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Models;
using Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Results;

using MediatR;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Handlers
{
    /// <summary>
    /// Get all spot symbols prices handler. Allows to get all cryptocurrency prices from the Binance.
    /// </summary>
    /// <remarks>
    /// Implements: <see cref="IRequestHandler{TRequest, TResponse}"/><br/>
    /// <c>TRequest</c> is <see cref="SymbolPricesModel"/><br/>
    /// <c>TResponse</c> is <see cref="SymbolPricesResult"/><br/>
    /// Is used by: MediatR
    /// </remarks>
    public class GetAllPricesHandler : IRequestHandler<SymbolPricesModel, SymbolPricesResult>
    {
        public async Task<SymbolPricesResult> Handle(SymbolPricesModel request, CancellationToken cancellationToken)
        {
            return new SymbolPricesResult
            {
                Prices = (await new BinanceClient().Spot.Market.GetPricesAsync(ct: CancellationToken.None)).Data
            };
        }
    }

    public class Haha
    {
        private static IEnumerable<BinanceSymbol> _symbols;

        public async Task<BinanceSymbol> GetSymbolByPair(string pair)
        {
            if (_symbols == null)
            {
                _symbols = (await new BinanceClient().Spot.System.GetExchangeInfoAsync()).Data.Symbols;
            }

            return _symbols.FirstOrDefault((sym) => sym.Name == pair);
        }
    }

}
