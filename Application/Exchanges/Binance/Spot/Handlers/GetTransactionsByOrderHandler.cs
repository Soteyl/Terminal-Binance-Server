using Binance.Net;
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
    /// Get transaction by concrete order id from binance spot market.
    /// </summary>
    /// <remarks>
    /// Implements: <see cref="IRequestHandler{TRequest, TResponse}"/><br/>
    /// <c>TRequest</c> is <see cref="GetTransactionsByOrderModel"/><br/>
    /// <c>TResponse</c> is <see cref="GetTransactionsByOrderResult"/><br/>
    /// Is used by: MediatR
    /// </remarks>
    public class GetTransactionsByOrderHandler : IRequestHandler<GetTransactionsByOrderModel, GetTransactionsByOrderResult>
    {
        private readonly CryptoTerminalContext _context;

        private readonly IHttpContextAccessor _contextAccessor;

        public GetTransactionsByOrderHandler(CryptoTerminalContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public async Task<GetTransactionsByOrderResult> Handle(GetTransactionsByOrderModel request, CancellationToken cancellationToken)
        {
            BinanceClient? client = new();
            string userId = _contextAccessor.GetCurrentUserId()!;

            ExchangeTokenEntity? token = await _context.ExchangeTokens.GetBinanceToken(userId);

            if (token == null)
                throw ServerException.MissingApiToken;

            client.SetApiCredentials(token.Key, token.Secret);

            WebCallResult<IEnumerable<BinanceTrade>>? result = await client.Spot.Order.GetUserTradesAsync(request.Symbol, request.OrderId);

            result.RemoveTokenAndThrowRestIfInvalid(_context, token);

            return new GetTransactionsByOrderResult
            {
                Trades = result.Data
            };
        }
    }
}
