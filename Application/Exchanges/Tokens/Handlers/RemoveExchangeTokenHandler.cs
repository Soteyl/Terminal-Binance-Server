using Microsoft.AspNetCore.Http;
using MediatR;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Handlers
{
    using Domain.Database.Models;
    using Exceptions;
    using EFData;
    using Models;

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

        public async Task<Unit> Handle(RemoveExchangeTokenQuery request, CancellationToken cancellationToken)
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

            return Unit.Value;
        }
    }
}
