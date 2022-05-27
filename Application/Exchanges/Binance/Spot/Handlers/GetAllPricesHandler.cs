﻿using Binance.Net;
using MediatR;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Handlers
{
    using Results;
    using Models;

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
                Prices = (await new BinanceClient().Spot.Market.GetPricesAsync()).Data
            };
        }
    }
}
