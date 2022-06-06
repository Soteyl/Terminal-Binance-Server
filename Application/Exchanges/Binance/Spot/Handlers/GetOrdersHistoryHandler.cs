using Microsoft.AspNetCore.Http;
using MediatR;
using Binance.Net;
using Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Results;
using Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Models;
using Ixcent.CryptoTerminal.Application.Exceptions;
using Ixcent.CryptoTerminal.Domain.Database.Models;
using Ixcent.CryptoTerminal.EFData;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Handlers
{

    /// <summary>
    /// Get all spot orders history handler. Allows to get orders history from the Binance.
    /// </summary>
    /// <remarks>
    /// Implements: <see cref="IRequestHandler{TRequest, TResponse}"/><br/>
    /// <c>TRequest</c> is <see cref="OrdersHistoryModel"/><br/>
    /// <c>TResponse</c> is <see cref="OrdersHistoryResult"/><br/>
    /// Is used by: MediatR
    /// </remarks>
    public class GetOrdersHistoryHandler : IRequestHandler<OrdersHistoryModel, OrdersHistoryResult>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        private readonly CryptoTerminalContext _context;

        /// <summary>
        /// Constructor for <see cref="GetOrdersHistoryHandler"/>.
        /// All the parameters in the contructor provided by the dependency injection.
        /// </summary>
        /// <param name="contextAccessor"> Context accessor which is required to get information about user. </param>
        /// <param name="context"> Allows to access tables in CryptoTerminal database. Required to access <see cref="ExchangeToken"/> for Binance. </param>
        public GetOrdersHistoryHandler(IHttpContextAccessor httpContext, CryptoTerminalContext context)
        {
            _context = context;
            _contextAccessor = httpContext;
        }

        public async Task<OrdersHistoryResult> Handle(OrdersHistoryModel request, CancellationToken cancellationToken)
        {
            var client = new BinanceClient();
            string userId = _contextAccessor.GetCurrentUserId()!;

            ExchangeToken? token = _context.ExchangeTokens.FirstOrDefault(t => t.UserId == userId &&
                                                                    t.Exchange == "Binance");
            if (token == null)
                throw RestException.MissingApiToken;

            client.SetApiCredentials(token.Key, token.Secret);

            var info = await client.Spot.Order.GetUserTradesAsync(request.Symbol.ToUpper());
            info.RemoveTokenAndThrowRestIfInvalid(_context, token);
            var trades = info.Data;

            return new OrdersHistoryResult
            {
                ClosedOrders = trades
            };
        }
    }
}
