using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Handlers
{
    using Domain.Database.Models;
    using EFData;
    using Models;
    using Validation;

    /// <summary> Handler for getting exchange tokens. </summary>
    /// <remarks>
    /// Implements <see cref="IRequestHandler{TRequest, TResponse}"/> <br/>
    /// <c>TRequest</c> is <see cref="GetExchangeTokensQuery"/> <br/>
    /// <c>TResponse</c> is <see cref="ExchangeTokensResult"/> <br/>
    /// </remarks>
    public class GetExchangeTokensHandler : IRequestHandler<GetExchangeTokensQuery, ExchangeTokensResult>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        private readonly ExchangesValidator _validator;

        private readonly CryptoTerminalContext _context;

        public GetExchangeTokensHandler(IHttpContextAccessor contextAccessor, CryptoTerminalContext context, ExchangesValidator validator)
        {
            _contextAccessor = contextAccessor;
            _context = context;
            _validator = validator;
        }

        public async Task<ExchangeTokensResult> Handle(GetExchangeTokensQuery request, CancellationToken cancellationToken)
        {
            ExchangeTokensResult result = new ExchangeTokensResult();

            string? userId = _contextAccessor.GetCurrentUserId();

            IQueryable<ExchangeToken>? tokens = _context.ExchangeTokens.Where(t => t.UserId == userId);

            foreach (ExchangeToken? token in tokens)
            {
                List<string>? list = await _validator.Validate(token.Key, token.Secret, token.Exchange);

                if (list.Count == 0)
                {
                    _context.ExchangeTokens.Remove(token);
                    await _context.SaveChangesAsync();
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
