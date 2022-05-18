using MediatR;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Handlers
{
    using Ixcent.CryptoTerminal.EFData;
    using Microsoft.AspNetCore.Http;
    using Models;
    using System.Threading;

    public class UpdateExchangeTokenHandler : IRequestHandler<UpdateExchangeTokenCommand, ExchangeTokenResult>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        private readonly CryptoTerminalContext _context;

        public UpdateExchangeTokenHandler(IHttpContextAccessor contextAccessor, CryptoTerminalContext context)
        {
            _contextAccessor = contextAccessor;
            _context = context;
        }

        public Task<ExchangeTokenResult> Handle(UpdateExchangeTokenCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _contextAccessor.GetCurrentUserId();
            var userTokenRecord = _context.ExchangeTokens.FirstOrDefault(x => x.UserId == currentUserId && x.Exchange == request.Exchange);

            if (userTokenRecord == null)
                return Task.FromResult(new ExchangeTokenResult
                {
                    IsError = true,
                    Message = "Unable to find specified exchange api token!"
                });


            userTokenRecord.Key = request.Token;
            userTokenRecord.Secret = request.Secret;

            _context.ExchangeTokens.Update(userTokenRecord);
            _context.SaveChanges();

            return Task.FromResult(new ExchangeTokenResult { 
                IsError = false, 
                Message = "Updated values for the specified token!"
            });
        }
    }
}
