using Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Models;
using Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Services;
using Ixcent.CryptoTerminal.Application.Mediatr;
using Ixcent.CryptoTerminal.Application.Validation;

using MediatR;

using Microsoft.AspNetCore.Http;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Handlers
{
    /// <summary> Handler for getting exchange tokens. </summary>
    public class GetExchangeTokensHandler : IRequestHandlerBase<GetExchangeTokensQuery, ExchangeTokensResult>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        private readonly ExchangesValidatorByToken _validator;

        private readonly IExchangeTokenService _service;

        public GetExchangeTokensHandler(IHttpContextAccessor contextAccessor, 
                                        IExchangeTokenService service)
        {
            _contextAccessor = contextAccessor;
            _service = service;
            _validator = ExchangesValidator.ByToken();
        }

        public async Task<Response<ExchangeTokensResult>> Handle(GetExchangeTokensQuery request,
            CancellationToken cancellationToken)
        {
            ExchangeTokensResult result = new();
            string? userId = _contextAccessor.GetCurrentUserId();

            Response<IEnumerable<CheckedExchangeToken>> tokens = await _service.GetTokensByUserId(userId);
            if (!tokens.IsSuccess)
                return Response.WithError<ExchangeTokensResult>(tokens.Error?.StatusCode);

            tokens.Result.ForEach(t =>
                result.AvailableExchanges.Add(t.Exchange, t.AvailableServices));

            return Response.Success(result);
        }
    }
}