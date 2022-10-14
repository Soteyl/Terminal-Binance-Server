﻿using Binance.Net;
using Binance.Net.Objects.Spot.SpotData;

using CryptoExchange.Net.Objects;

using Ixcent.CryptoTerminal.Application.Exceptions;
using Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Models;
using Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Results;
using Ixcent.CryptoTerminal.Domain.Database.Models;
using Ixcent.CryptoTerminal.StorageHandle;

using MediatR;

using Microsoft.AspNetCore.Http;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Handlers
{
    /// <summary>
    /// Get all open spot orders handler. Allows to get all active orders from the Binance.
    /// </summary>
    /// <remarks>
    /// Implements: <see cref="IRequestHandler{TRequest, TResponse}"/><br/>
    /// <c>TRequest</c> is <see cref="OpenOrdersModel"/><br/>
    /// <c>TResponse</c> is <see cref="OpenOrdersResult"/><br/>
    /// Is used by: MediatR
    /// </remarks>
    public class GetOpenOrdersHandler : IRequestHandler<OpenOrdersModel, OpenOrdersResult>
    {
        private readonly CryptoTerminalContext _context;

        private readonly IHttpContextAccessor _contextAccessor;

        public GetOpenOrdersHandler(CryptoTerminalContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public async Task<OpenOrdersResult> Handle(OpenOrdersModel request, CancellationToken cancellationToken)
        {
            BinanceClient? client = new();
            string userId = _contextAccessor.GetCurrentUserId()!;

            ExchangeTokenEntity? token = _context.ExchangeTokens.FirstOrDefault(t => t.UserId == userId &&
                                                                    t.Exchange == "Binance");

            if (token == null)
                throw ServerException.MissingApiToken;

            client.SetApiCredentials(token.Key, token.Secret);

            WebCallResult<IEnumerable<BinanceOrder>>? result = await client.Spot.Order.GetOpenOrdersAsync(ct: CancellationToken.None);

            result.RemoveTokenAndThrowRestIfInvalid(_context, token);

            return new OpenOrdersResult
            {
                Orders = result.Data
            };
        }
    }
}
