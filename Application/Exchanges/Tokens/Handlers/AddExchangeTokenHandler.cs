using Microsoft.AspNetCore.Http;
using System.Net;
using MediatR;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Handlers
{
    using Domain.Database.Models;
    using Exceptions;
    using Validation;
    using EFData;
    using Models;

    /// <summary> Handler for updating exchange token into a database. </summary>
    /// <remarks>
    /// Implements <see cref="IRequestHandler{TRequest}"/> <br/>
    /// <c>TRequest</c> is <see cref="AddExchangeTokenQuery"/> <br/>
    /// </remarks>
    public class AddExchangeTokenHandler : IRequestHandler<AddExchangeTokenQuery>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        private readonly ExchangesValidatorByToken _validator;

        private readonly CryptoTerminalContext _context;

        public AddExchangeTokenHandler(IHttpContextAccessor contextAccessor,
                                       CryptoTerminalContext context)
        {
            _contextAccessor = contextAccessor;
            _context = context;
            _validator = new ExchangesValidator().ByToken();
        }

        public async Task<Unit> Handle(AddExchangeTokenQuery request, CancellationToken cancellationToken)
        {
            string currentUserId = _contextAccessor.GetCurrentUserId();

            if ((await _validator.Validate(request.Key, request.Secret, request.Exchange)).Count() == 0)
                throw new RestException(HttpStatusCode.BadRequest, ErrorCode.BadExchangeToken, new
                {
                    Message = "Bad key or secret"
                });

            ExchangeToken? existingToken = _context.ExchangeTokens.FirstOrDefault(
                t => request.Exchange == t.Exchange &&
                currentUserId == t.UserId);

            if (existingToken != null)
            {
                existingToken.Key = request.Key;
                existingToken.Secret = request.Secret;
                _context.ExchangeTokens.Update(existingToken);
            }
            else
            {
                _context.ExchangeTokens.Add(
                    new ExchangeToken
                    {
                        Exchange = request.Exchange,
                        Key = request.Key,
                        Secret = request.Secret,
                        UserId = currentUserId
                    });
            }

            _context.SaveChanges();

            return Unit.Value;
        }
    }
}
