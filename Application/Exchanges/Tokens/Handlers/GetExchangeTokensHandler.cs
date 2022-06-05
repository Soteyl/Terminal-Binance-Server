using Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Models;
using Ixcent.CryptoTerminal.Application.Validation;
using Ixcent.CryptoTerminal.Domain.Database.Models;
using Ixcent.CryptoTerminal.EFData;

using MediatR;

using Microsoft.AspNetCore.Http;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Handlers
{
    /// <summary> Handler for getting exchange tokens. </summary>
    /// <remarks>
    /// Implements <see cref="IRequestHandler{TRequest, TResponse}"/> <br/>
    /// <c>TRequest</c> is <see cref="GetExchangeTokensQuery"/> <br/>
    /// <c>TResponse</c> is <see cref="ExchangeTokensResult"/> <br/>
    /// </remarks>
    public class GetExchangeTokensHandler : IRequestHandler<GetExchangeTokensQuery, ExchangeTokensResult>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        private readonly ExchangesValidatorByToken _validator;

        private readonly CryptoTerminalContext _context;

        public GetExchangeTokensHandler(IHttpContextAccessor contextAccessor, CryptoTerminalContext context)
        {
            _contextAccessor = contextAccessor;
            _context = context;
            _validator = ExchangesValidator.ByToken();
        }

        public async Task<ExchangeTokensResult> Handle(GetExchangeTokensQuery request, CancellationToken cancellationToken)
        {
            ExchangeTokensResult result = new ExchangeTokensResult();

            string? userId = _contextAccessor.GetCurrentUserId();

            IQueryable<ExchangeToken>? tokens = _context.ExchangeTokens.Where(t => t.UserId == userId);

            foreach (ExchangeToken? token in tokens)
            {
                IEnumerable<string>? list = await _validator.Validate(token.Key, token.Secret, token.Exchange);

                if (list.Any() == false)
                {
                    _context.ExchangeTokens.Remove(token);
                    await _context.SaveChangesAsync(CancellationToken.None);
                }
                else
                {
                    result.AvailableExchanges.Add(token.Exchange, list);
                }
            }

            return result;
        }
    }
}
