using Ixcent.CryptoTerminal.Application.Exceptions;
using Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Models;
using Ixcent.CryptoTerminal.Domain.Database.Models;
using Ixcent.CryptoTerminal.EFData;

using MediatR;

using Microsoft.AspNetCore.Http;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Handlers
{
    /// <summary> Handler for removing exchange tokens. </summary>
    /// <remarks>
    /// Implements <see cref="IRequestHandler{TRequest}"/> <br/>
    /// <c>TRequest</c> is <see cref="RemoveExchangeTokenQuery"/> <br/>
    /// </remarks>
    public class RemoveExchangeTokenHandler : IRequestHandler<RemoveExchangeTokenQuery>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        private readonly CryptoTerminalContext _context;

        public RemoveExchangeTokenHandler(IHttpContextAccessor contextAccessor, CryptoTerminalContext context)
        {
            _contextAccessor = contextAccessor;
            _context = context;
        }

        public Task<Unit> Handle(RemoveExchangeTokenQuery request, CancellationToken cancellationToken)
        {
            string exchange = request.Exchange;
            string userId = _contextAccessor.GetCurrentUserId();

            ExchangeToken? possibleToken =
                _context.ExchangeTokens.FirstOrDefault(t => exchange == t.Exchange
                                                      && userId == t.UserId);

            if (possibleToken == null)
            {
                throw new RestException(System.Net.HttpStatusCode.BadRequest, ErrorCode.NotFound, new
                {
                    Message = "Couldn't find a token"
                });
            }

            _context.ExchangeTokens.Remove(possibleToken);

            _context.SaveChanges();

            return Task.FromResult(Unit.Value);
        }
    }
}
