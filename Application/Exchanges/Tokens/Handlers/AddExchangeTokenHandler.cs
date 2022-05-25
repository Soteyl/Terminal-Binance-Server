using MediatR;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Handlers
{
    using Domain.Database.Models;
    using EFData;
    using Exceptions;
    using Models;
    using Validation;

    /// <summary> Handler for adding exchange token into a database. </summary>
    /// <remarks>
    /// Implements <see cref="IRequestHandler{TRequest}"/> <br/>
    /// <c>TRequest</c> is <see cref="AddExchangeTokenQuery"/> <br/>
    /// </remarks>
    public class AddExchangeTokenHandler : IRequestHandler<AddExchangeTokenQuery>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        private readonly ExchangesValidator _validator;

        private readonly CryptoTerminalContext _context;

        public AddExchangeTokenHandler(IHttpContextAccessor contextAccessor,
                                       CryptoTerminalContext context,
                                       ExchangesValidator validator)
        {
            _contextAccessor = contextAccessor;
            _context = context;
            _validator = validator;
        }

        public async Task<Unit> Handle(AddExchangeTokenQuery request, CancellationToken cancellationToken)
        {
            string currentUserId = _contextAccessor.GetCurrentUserId();

            if (currentUserId == null)
                throw new RestException(HttpStatusCode.BadRequest, new
                {
                    Message = "Invalid user"
                });

            if (_context.ExchangeTokens.FirstOrDefault(t => request.Exchange == t.Exchange && currentUserId == t.UserId) != null)
                throw new RestException(HttpStatusCode.BadRequest, new
                {
                    Message = "Specified api token already exists"
                });

            if ((await _validator.Validate(request.Key, request.Secret, request.Exchange)).Count == 0)
                throw new RestException(HttpStatusCode.BadRequest, new
                {
                    Message = "Bad key or secret"
                });

            _context.ExchangeTokens.Add(
                new ExchangeToken
                {
                    Exchange = request.Exchange,
                    Key = request.Key,
                    Secret = request.Secret,
                    UserId = currentUserId
                });

            _context.SaveChanges();

            return Unit.Value;
        }
    }
}
