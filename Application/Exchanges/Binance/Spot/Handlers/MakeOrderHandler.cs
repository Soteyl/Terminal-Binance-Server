using Binance.Net;
using Binance.Net.Objects.Spot.SpotData;

using CryptoExchange.Net.Objects;

using Ixcent.CryptoTerminal.Application.Exceptions;
using Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Models;
using Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Results;
using Ixcent.CryptoTerminal.Domain.Database.Models;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Data;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Repository;
using Ixcent.CryptoTerminal.StorageHandle;

using MediatR;

using Microsoft.AspNetCore.Http;


namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Handlers
{
    /// <summary> Handler for making Binance spot orders. </summary>
    /// <remarks>
    /// Implements <see cref="IRequestHandler{TKey, TValue}"/> <br/>
    /// <c>TRequest</c> is <see cref="MakeOrderModel"/> <br/>
    /// <c>TResponse</c> is <see cref="MakeOrderResult"/>
    /// </remarks>
    public class MakeOrderHandler : IRequestHandler<MakeOrderModel, MakeOrderResult>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        private readonly CryptoTerminalContext _context;

        /// <summary>
        /// Constructor for <see cref="GetAllBalancesHandler"/>.
        /// All the parameters in the contructor provided by the dependency injection.
        /// </summary>
        /// <param name="httpContextAccessor"> Context accessor which is required to get information about user. </param>
        /// <param name="context"> Allows to access tables in CryptoTerminal database. Required to access <see cref="ExchangeToken"/> for Binance. </param>
        public MakeOrderHandler(IHttpContextAccessor httpContextAccessor, CryptoTerminalContext context)
        {
            _contextAccessor = httpContextAccessor;
            _context = context;
        }

        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ServerException"></exception>
        public async Task<MakeOrderResult> Handle(MakeOrderModel request, CancellationToken cancellationToken)
        {
            BinanceClient client = new();

            string userId = _contextAccessor.GetCurrentUserId()!;

            ExchangeTokenEntity? token = _context.ExchangeTokens.FirstOrDefault(token => token.UserId == userId &&
                                                                        token.Exchange.Equals("Binance"));

            if (token == null)
                throw ServerException.MissingApiToken;

            client.SetApiCredentials(token.Key, token.Secret);

            WebCallResult<BinancePlacedOrder> info = await client.Spot.Order.PlaceOrderAsync(
                symbol: request.Symbol,
                side: request.OrderSide,
                type: request.OrderType,
                quantity: request.Quantity,
                timeInForce: request.TimeInForce,
                price: request.Price,
                icebergQty: request.IcebergQuantity,
                stopPrice: request.StopPrice,
                ct: cancellationToken
                );

            info.RemoveTokenAndThrowRestIfInvalid(_context, token);

            return new MakeOrderResult
            {
                PlacedOrder = info.Data
            };
        }
    }
}
