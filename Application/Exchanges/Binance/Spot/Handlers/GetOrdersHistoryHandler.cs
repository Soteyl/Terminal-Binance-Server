using Microsoft.AspNetCore.Http;
using MediatR;
using Binance.Net;
using Binance.Net.Objects.Spot.SpotData;

using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Objects;

using Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Results;
using Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Models;
using Ixcent.CryptoTerminal.Application.Exceptions;
using Ixcent.CryptoTerminal.Domain.Database.Models;
using Ixcent.CryptoTerminal.StorageHandle;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Handlers
{
    /// <summary>
    /// Get all spot orders history handler including open orders. Allows to get orders history from the Binance.
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
        /// <param name="context"> Allows to access tables in CryptoTerminal database. Required to access <see cref="ExchangeTokenEntity"/> for Binance. </param>
        public GetOrdersHistoryHandler(IHttpContextAccessor httpContext, CryptoTerminalContext context)
        {
            _context = context;
            _contextAccessor = httpContext;
        }

        public async Task<OrdersHistoryResult> Handle(OrdersHistoryModel request, CancellationToken cancellationToken)
        {
            BinanceClient? client = new();
            string userId = _contextAccessor.GetCurrentUserId()!;

            ExchangeTokenEntity? token = _context.ExchangeTokens.FirstOrDefault(t => t.UserId == userId &&
                                                                    t.Exchange == "Binance");
            if (token == null)
                throw ServerException.MissingApiToken;

            client.SetApiCredentials(token.Key, token.Secret);

            WebCallResult<IEnumerable<BinanceOrder>>? info = await client.Spot.Order.GetOrdersAsync(request.Symbol.ToUpper());
            
            info.RemoveTokenAndThrowRestIfInvalid(_context, token);

            IEnumerable<BinanceOrder>? ordersHistory = info.Data;

            return new OrdersHistoryResult
            {
                Orders = ordersHistory
            };
        }
    }
}
