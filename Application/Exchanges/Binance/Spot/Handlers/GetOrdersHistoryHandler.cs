
using Binance.Net;
using MediatR;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.ExchangeInterfaces;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Handlers
{
    using System.Threading;
    using Results;
    using Models;
    using Microsoft.AspNetCore.Http;
    using Ixcent.CryptoTerminal.EFData;
    using Ixcent.CryptoTerminal.Domain.Database.Models;
    using Ixcent.CryptoTerminal.Application.Exceptions;

    public class GetOrdersHistoryHandler : IRequestHandler<OrdersHistoryModel, OrdersHistoryResult>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        private readonly CryptoTerminalContext _context;

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

            WebCallResult<IEnumerable<ICommonSymbol>> info = await (client as IExchangeClient).GetSymbolsAsync();

            info.RemoveTokenAndThrowRestIfInvalid(_context, token);

            return new OrdersHistoryResult
            {
                Symbols = info.Data
            };
        }
    }
}
