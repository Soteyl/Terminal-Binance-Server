using MediatR;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Handlers
{
    using Domain.Database.Models;
    using Ixcent.CryptoTerminal.EFData;
    using Microsoft.AspNetCore.Http;
    using Models;
    using System.Threading;
    using System.Threading.Tasks;

    public class AddExchangeTokenHandler : IRequestHandler<AddExchangeTokenCommand, ExchangeTokenResult>
    {

        private readonly IHttpContextAccessor _contextAccessor;

        private readonly CryptoTerminalContext _context;

        public AddExchangeTokenHandler(IHttpContextAccessor contextAccessor, CryptoTerminalContext context)
        {
            _contextAccessor = contextAccessor;
            _context = context;
        }

        public Task<ExchangeTokenResult> Handle(AddExchangeTokenCommand request, CancellationToken cancellationToken)
        {
            string currentUserId = _contextAccessor.GetCurrentUserId();
            
            if (currentUserId == null)
                return Task.FromResult(new ExchangeTokenResult
                {
                    IsError = true,
                    Message = "Invalid user id!"
                });

            if (_context.ExchangeTokens.FirstOrDefault(t => request.Exchange == t.Exchange && currentUserId == t.UserId) != null)
                return Task.FromResult(new ExchangeTokenResult
                {
                    IsError = true,
                    Message = "Specified api token already exists!"
                });

            _context.ExchangeTokens.Add(
                new ExchangeToken
                {
                    Exchange = request.Exchange,
                    Key = request.Token,
                    Secret = request.Secret,
                    UserId = currentUserId
                });

            _context.SaveChanges();


            return Task.FromResult(new ExchangeTokenResult
                {
                    IsError = false,
                    Message = "Successfully added new api token!"
                });
        }
    }
}
