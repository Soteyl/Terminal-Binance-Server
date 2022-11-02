using Ixcent.CryptoTerminal.Application.Validation;
using Ixcent.CryptoTerminal.Domain.Common.Extensions;
using Ixcent.CryptoTerminal.Domain.Common.Interfaces;
using Ixcent.CryptoTerminal.Domain.Common.Models;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Interfaces;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Contracts;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Service;

using Microsoft.AspNetCore.Http;

namespace Ixcent.CryptoTerminal.Application.ExchangeTokens.Handlers
{
    /// <summary> Handler for getting exchange tokens. </summary>
    public class GetExchangeTokensHandler : IRequestHandlerBase<GetExchangeTokensQuery, GetExchangeTokensResponse>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        private readonly ExchangesValidatorByToken _validator;

        private readonly IExchangeTokenService _tokenService;

        public GetExchangeTokensHandler(IHttpContextAccessor contextAccessor, IExchangeTokenService service)
        {
            _contextAccessor = contextAccessor;
            _tokenService = service;
            _validator = ExchangesValidator.ByToken();
        }

        public async Task<Response<GetExchangeTokensResponse>> Handle(GetExchangeTokensQuery request, CancellationToken cancellationToken)
        {
            GetExchangeTokensResponse response = new();
            string? userId = _contextAccessor.GetCurrentUserId();

            Response<IEnumerable<CheckedExchangeToken>> tokens = await _tokenService.Get(new UserId() { Value = userId }, cancellationToken);

            if (!tokens.IsSuccess)
                return Response.WithError<GetExchangeTokensResponse>(tokens.Error?.StatusCode);

            tokens.Result.ForEach(t => response.AvailableExchanges.Add(t.Exchange, t.AvailableServices));

            return Response.Success(response);
        }
    }
}