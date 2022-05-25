using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Handlers
{
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

        public Task<MakeOrderResult> Handle(MakeOrderModel request, CancellationToken cancellationToken)
        {
            //TODO its a test method and it must be implemented later
            string a = _contextAccessor.GetCurrentUserId();
            return null;
        }
    }
}
