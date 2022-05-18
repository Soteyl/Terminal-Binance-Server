using MediatR;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Handlers
{
    using Ixcent.CryptoTerminal.Application.Exceptions;
    using Ixcent.CryptoTerminal.EFData;
    using Microsoft.AspNetCore.Http;
    using Models;
    using System.Threading;
    using System.Threading.Tasks;

    public class RemoveExchangeTokenHandler : IRequestHandler<RemoveExchangeTokenQuery>
    {

        private readonly IHttpContextAccessor _contextAccessor;

        private readonly CryptoTerminalContext _context;

        public RemoveExchangeTokenHandler(IHttpContextAccessor contextAccessor, CryptoTerminalContext context)
        {
            _contextAccessor = contextAccessor;
            _context = context;
        }

        public async Task<Unit> Handle(RemoveExchangeTokenQuery request, CancellationToken cancellationToken)
        {
            string exchange = request.Exchange;
            string userId = _contextAccessor.GetCurrentUserId();

            var possibleToken = _context.ExchangeTokens.FirstOrDefault(t => exchange == t.Exchange && userId == t.UserId);

            if (possibleToken == null)
                throw new RestException(System.Net.HttpStatusCode.BadRequest, new {
                    Message = "Couldn't find a token."
                });

            _context.ExchangeTokens.Remove(possibleToken);

            _context.SaveChanges();

            return Unit.Value;
        }
    }
}
