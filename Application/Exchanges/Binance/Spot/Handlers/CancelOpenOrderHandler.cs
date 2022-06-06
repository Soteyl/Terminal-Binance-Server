using MediatR;
using Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Results;
using Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Models;
using Ixcent.CryptoTerminal.EFData;
using Microsoft.AspNetCore.Http;
using Binance.Net;
using Ixcent.CryptoTerminal.Domain.Database.Models;
using Ixcent.CryptoTerminal.Application.Exceptions;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Handlers
{
    /// <summary>
    /// Handler for canceling open orders on binance spot.
    /// </summary>
    public class CancelOpenOrderHandler : IRequestHandler<CancelOpenOrderModel, CancelOpenOrderResult>
    {
        private readonly CryptoTerminalContext _context;

        private readonly IHttpContextAccessor _contextAccessor;

        public CancelOpenOrderHandler(CryptoTerminalContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }
        
        public async Task<CancelOpenOrderResult> Handle(CancelOpenOrderModel request, CancellationToken cancellationToken)
        {
            var client = new BinanceClient();
            string userId = _contextAccessor.GetCurrentUserId()!;

            ExchangeToken? token = _context.ExchangeTokens.FirstOrDefault(t => t.UserId == userId &&
                                                                    t.Exchange == "Binance");
            if (token == null)
                throw RestException.MissingApiToken;

            client.SetApiCredentials(token.Key, token.Secret);

            var result = await client.Spot.Order.CancelOrderAsync(request.Symbol, request.Id);

            result.RemoveTokenAndThrowRestIfInvalid(_context, token);

            return new CancelOpenOrderResult
            {
                CanceledOrder = result.Data
            };
        }
    }
}
