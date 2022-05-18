using MediatR;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Handlers
{
    using Ixcent.CryptoTerminal.Application.Exceptions;
    using Ixcent.CryptoTerminal.Application.Validation;
    using Ixcent.CryptoTerminal.EFData;
    using Microsoft.AspNetCore.Http;
    using Models;
    using System.Threading;

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
            var currentUserId = _contextAccessor.GetCurrentUserId();
            var userTokenRecord = _context.ExchangeTokens.FirstOrDefault(x => x.UserId == currentUserId && x.Exchange == request.Exchange);

            if (userTokenRecord == null)
                throw new RestException(System.Net.HttpStatusCode.BadRequest, new {
                    Message = "Invalid user."
                });


            if ((await _validator.Validate(request.Key, request.Secret, request.Exchange)).Count == 0)
                throw new RestException(System.Net.HttpStatusCode.BadRequest, new
                {
                    Message = "Bad key or secret!"
                });


            userTokenRecord.Key = request.Key;
            userTokenRecord.Secret = request.Secret;

            _context.ExchangeTokens.Update(userTokenRecord);
            _context.SaveChanges();

            return Unit.Value;
        }
    }
}
