using Ixcent.CryptoTerminal.Application.Validation;
using Ixcent.CryptoTerminal.Domain.Common.Extensions;
using Ixcent.CryptoTerminal.Domain.Common.Interfaces;
using Ixcent.CryptoTerminal.Domain.Common.Models;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Interfaces;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Handler;
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

        public async Task<Response<GetExchangeTokensResponse>> Handle(GetExchangeTokensQuery request,
            CancellationToken cancellationToken)
        {
            GetExchangeTokensResponse response = new();
            string? userId = _contextAccessor.GetCurrentUserId();

            Response<GetTokensResponse> serviceResponse =
                await _tokenService.Get(new GetTokensRequest { UserId = userId }, cancellationToken);

            if (!serviceResponse.IsSuccess)
                return Response.WithError<GetExchangeTokensResponse>(serviceResponse.Error?.StatusCode);

            serviceResponse.Result?.Tokens.ForEach(
                t => response.AvailableExchanges.Add(t.Exchange, t.AvailableServices));

            return Response.Success(response);
        }
    }
}