using Microsoft.AspNetCore.Http;
using Binance.Net;
using Binance.Net.Objects.Spot.SpotData;

using CryptoExchange.Net.Objects;

using MediatR;
using Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Results;
using Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Models;
using Ixcent.CryptoTerminal.Domain.Database.Models;
using Ixcent.CryptoTerminal.Application.Exceptions;
using Ixcent.CryptoTerminal.StorageHandle;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Handlers
{

    /// <summary>
    /// Cancel open binance spot order.
    /// </summary>
    /// <remarks>
    /// Implements: <see cref="IRequestHandler{TRequest, TResponse}"/><br/>
    /// <c>TRequest</c> is <see cref="CancelOpenOrderModel"/><br/>
    /// <c>TResponse</c> is <see cref="CancelOpenOrderResult"/><br/>
    /// Is used by: MediatR
    /// </remarks>
    public class CancelOpenOrderHandler : IRequestHandler<CancelOpenOrderModel, CancelOpenOrderResult>
    {
        private readonly CryptoTerminalContext _context;

        private readonly IHttpContextAccessor _contextAccessor;

        /// <summary>
        /// Constructor for <see cref="CancelOpenOrderHandler"/>.
        /// All the parameters in the contructor provided by the dependency injection.
        /// </summary>
        /// <param name="contextAccessor"> Context accessor which is required to get information about user. </param>
        /// <param name="context"> Allows to access tables in CryptoTerminal database. Required to access <see cref="ExchangeTokenEntity"/> for Binance. </param>
        public CancelOpenOrderHandler(CryptoTerminalContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public async Task<CancelOpenOrderResult> Handle(CancelOpenOrderModel request, CancellationToken cancellationToken)
        {
            BinanceClient? client = new();
            string userId = _contextAccessor.GetCurrentUserId()!;

            ExchangeTokenEntity? token = _context.ExchangeTokens.FirstOrDefault(t => t.UserId == userId &&
                                                                    t.Exchange == "Binance");
            if (token == null)
                throw ServerException.MissingApiToken;

            client.SetApiCredentials(token.Key, token.Secret);

            WebCallResult<BinanceCanceledOrder>? result = await client.Spot.Order.CancelOrderAsync(
                symbol: request.Symbol!, 
                orderId: request.Id!
                );

            result.RemoveTokenAndThrowRestIfInvalid(_context, token);

            return new CancelOpenOrderResult
            {
                CanceledOrder = result.Data
            };
        }
    }
}
