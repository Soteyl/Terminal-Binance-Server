using Binance.Net;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Handlers
{
    using EFData;
    using Models;
    using Results;
    using Validation;

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
            var binanceClient = new BinanceClient(); //TODO
            string userId = _contextAccessor.GetCurrentUserId()!;

            TokenValidationResult? result = await new ExchangesValidator()
                .ByUser(_context, userId).Binance().Spot().ValidateOrThrowRest();

            return null;
        }
    }
}
