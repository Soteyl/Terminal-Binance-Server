using MediatR;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Handlers
{
    using Ixcent.CryptoTerminal.EFData;
    using Microsoft.AspNetCore.Http;
    using Models;
    using System.Threading;
    using System.Threading.Tasks;

    public class RemoveExchangeTokenHandler : IRequestHandler<RemoveExchangeTokenCommand, ExchangeTokenResult>
    {

        private readonly IHttpContextAccessor _contextAccessor;

        private readonly CryptoTerminalContext _context;

        public RemoveExchangeTokenHandler(IHttpContextAccessor contextAccessor, CryptoTerminalContext context)
        {
            _contextAccessor = contextAccessor;
            _context = context;
        }

        public Task<ExchangeTokenResult> Handle(RemoveExchangeTokenCommand request, CancellationToken cancellationToken)
        {
            string exchange = request.Exchange;
            string userId = _contextAccessor.GetCurrentUserId();

            var possibleToken = _context.ExchangeTokens.FirstOrDefault(t => exchange == t.Exchange && userId == t.UserId);

            if (possibleToken == null)
                return Task.FromResult(new ExchangeTokenResult
                {
                    IsError = true, 
                    Message = "No api token for such exchange was found!"
                });

            _context.ExchangeTokens.Remove(possibleToken);

            _context.SaveChanges();

            return Task.FromResult(new ExchangeTokenResult
                    {
                        IsError = false,
                        Message = "Successfully deleted specified token!"
                    });
        }
    }
}
