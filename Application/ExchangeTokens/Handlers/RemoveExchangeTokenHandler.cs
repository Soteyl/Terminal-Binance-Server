using Ixcent.CryptoTerminal.Domain.Common.Interfaces;
using Ixcent.CryptoTerminal.Domain.Common.Models;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Interfaces;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Contracts;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Service;

using MediatR;

using Microsoft.AspNetCore.Http;

namespace Ixcent.CryptoTerminal.Application.ExchangeTokens.Handlers
{
    /// <summary> Handler for removing exchange tokens. </summary>
    /// <remarks>
    /// Implements <see cref="IRequestHandler{TRequest}"/> <br/>
    /// <c>TRequest</c> is <see cref="RemoveExchangeTokenQuery"/> <br/>
    /// </remarks>
    public class RemoveExchangeTokenHandler : IRequestHandlerBase<RemoveExchangeTokenQuery>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        private readonly IExchangeTokenService _service;

        public RemoveExchangeTokenHandler(IHttpContextAccessor contextAccessor, IExchangeTokenService service)
        {
            _contextAccessor = contextAccessor;
            _service = service;
        }

        public async Task<Response> Handle(RemoveExchangeTokenQuery request, CancellationToken cancellationToken)
        {
            string userId = _contextAccessor.GetCurrentUserId();

            var userExchange = new UserExchange
            {
                Exchange = request.Exchange,
                UserId = userId
            };

            Response response = await _service.RemoveToken(userExchange, cancellationToken);

            return response;
        }
    }
}