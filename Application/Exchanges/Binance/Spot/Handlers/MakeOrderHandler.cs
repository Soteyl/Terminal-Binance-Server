using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Handlers
{
    using CryptoExchange.Net.Objects;
    using global::Binance.Net;
    using global::Binance.Net.Objects.Spot.SpotData;
    using Ixcent.CryptoTerminal.Application.Exceptions;
    using Ixcent.CryptoTerminal.EFData;
    using Models;
    using Results;

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

        public MakeOrderHandler(IHttpContextAccessor httpContextAccessor, CryptoTerminalContext context)
        {
            _contextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<MakeOrderResult> Handle(MakeOrderModel request, CancellationToken cancellationToken)
        {
            BinanceClient client = new BinanceClient();

            string userId = _contextAccessor.GetCurrentUserId()!;

            var token = _context.ExchangeTokens.FirstOrDefault(token => token.UserId == userId &&
                                                                        token.Exchange.Equals("Binance"));

            if (token == null)
                throw new RestException(System.Net.HttpStatusCode.BadRequest,
                                        ErrorCode.BadExchangeToken,
                                        new { Token = "Missing API token" });

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

            };
        }
    }
}
