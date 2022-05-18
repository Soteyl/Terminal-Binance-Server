using MediatR;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Handlers
{
    using Models;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Database.Models;
    using Ixcent.CryptoTerminal.Application.Exceptions;
    using Ixcent.CryptoTerminal.Application.Validation;
    using Ixcent.CryptoTerminal.EFData;
    using Microsoft.AspNetCore.Http;

    public class AddExchangeTokenHandler : IRequestHandler<AddExchangeTokenQuery>
    {

        private readonly IHttpContextAccessor _contextAccessor;

        private readonly ExchangesValidator _validator;

        private readonly CryptoTerminalContext _context;

        public AddExchangeTokenHandler(IHttpContextAccessor contextAccessor, CryptoTerminalContext context, ExchangesValidator validator)
        {
            _contextAccessor = contextAccessor;
            _context = context;
            _validator = validator;
        }

        public async Task<Unit> Handle(AddExchangeTokenQuery request, CancellationToken cancellationToken)
        {
            string currentUserId = _contextAccessor.GetCurrentUserId();

            if (currentUserId == null)
                throw new RestException(System.Net.HttpStatusCode.BadRequest, new {
                    Message = "Invalid user id!"
                });

            if (_context.ExchangeTokens.FirstOrDefault(t => request.Exchange == t.Exchange && currentUserId == t.UserId) != null)
                throw new RestException(System.Net.HttpStatusCode.BadRequest, new {
                    Message = "Specified api token already exists!"
                });

            if ((await _validator.Validate(request.Key, request.Secret, request.Exchange)).Count == 0)
                throw new RestException(System.Net.HttpStatusCode.BadRequest, new {
                    Message = "Bad key or secret!"
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
