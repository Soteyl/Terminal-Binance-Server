using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Handlers
{
    using Ixcent.CryptoTerminal.EFData;
    using Models;

    /// <summary> Handler for making Binance spot orders. </summary>
    /// <remarks>
    /// Implements <see cref="IRequestHandler{TKey, TValue}"/> <br/>
    /// <c>TRequest</c> is <see cref="MakeSpotOrderModel"/> <br/>
    /// <c>TResponse</c> is <see cref="MakeSpotOrderResult"/>
    /// </remarks>
    public class MakeSpotOrderHandler : IRequestHandler<MakeSpotOrderModel, MakeSpotOrderResult>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        private readonly CryptoTerminalContext _context;

        public MakeSpotOrderHandler(IHttpContextAccessor httpContextAccessor, CryptoTerminalContext context)
        {
            _contextAccessor = httpContextAccessor;
            _context = context;
        }

        public Task<MakeSpotOrderResult> Handle(MakeSpotOrderModel request, CancellationToken cancellationToken)
        {
            //TODO its a test method and it must be implemented later
            string a = _contextAccessor.GetCurrentUserId();
            return null;
        }
    }
}
