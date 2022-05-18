using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Handlers
{
    using Ixcent.CryptoTerminal.Application.Validation;
    using Ixcent.CryptoTerminal.EFData;
    using Microsoft.AspNetCore.Http;
    using Models;
    using System.Threading;


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

            var userId = _contextAccessor.GetCurrentUserId();

            var tokens = _context.ExchangeTokens.Where(t => t.UserId == userId);

            foreach (var token in tokens)
            {
                var list = await _validator.Validate(token.Key, token.Secret, token.Exchange);

                if (list.Count == 0)
                    _context.ExchangeTokens.Remove(token);
                else
                    result.AvailableExchanges.Add(token.Exchange, list);
            }

            return result;
        }
    }
}
