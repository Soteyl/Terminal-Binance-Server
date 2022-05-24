using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Handlers
{
    using Domain.Database.Models;
    using EFData;
    using Exceptions;
    using Models;
    using Validation;

    /// <summary> Handler for editing exchange tokens.</summary>
    /// <remarks>
    /// Implements <see cref="IRequestHandler{TRequest}"/> <br/>
    /// <c>TRequest</c> is <see cref="UpdateExchangeTokenQuery"/> <br/>
    /// </remarks>
    public class UpdateExchangeTokenHandler : IRequestHandler<UpdateExchangeTokenQuery>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        private readonly CryptoTerminalContext _context;

        private readonly ExchangesValidator _validator;

        public UpdateExchangeTokenHandler(IHttpContextAccessor contextAccessor, CryptoTerminalContext context, ExchangesValidator validator)
        {
            _contextAccessor = contextAccessor;
            _context = context;
            _validator = validator;
        }

        public async Task<Unit> Handle(UpdateExchangeTokenQuery request, CancellationToken cancellationToken)
        {
            string? currentUserId = _contextAccessor.GetCurrentUserId();
            ExchangeToken? userTokenRecord =
                _context.ExchangeTokens.FirstOrDefault(x => x.UserId == currentUserId
                                                       && x.Exchange == request.Exchange);

            if (userTokenRecord == null)
            {
                throw new RestException(System.Net.HttpStatusCode.BadRequest, new
                {
                    Message = "Invalid user"
                });
            }


            if ((await _validator.Validate(request.Key, request.Secret, request.Exchange)).Count == 0)
            {
                throw new RestException(System.Net.HttpStatusCode.BadRequest, new
                {
                    Message = "Bad key or secret"
                });
            }

            userTokenRecord.Key = request.Key;
            userTokenRecord.Secret = request.Secret;

            _context.ExchangeTokens.Update(userTokenRecord);
            _context.SaveChanges();

            return Unit.Value;
        }
    }
}
