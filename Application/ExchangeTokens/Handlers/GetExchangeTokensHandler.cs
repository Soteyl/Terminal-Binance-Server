using AutoMapper;

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
        private readonly IExchangeTokenService _tokenService;
        
        private readonly IMapper _mapper;
        
        private readonly IValidatorResolver _validator;

        public GetExchangeTokensHandler(IExchangeTokenService service, IMapper mapper, IValidatorResolver validatorResolver)
        {
            _tokenService = service;
            _mapper = mapper;
            _validator = validatorResolver;
        }

        public async Task<Response<GetExchangeTokensResponse>> Handle(GetExchangeTokensQuery request,
            CancellationToken cancellationToken)
        {
            await _validator.ValidateAsync(request, cancellationToken);
            
            var requestToken = _mapper.Map<GetExchangeTokensRequest>(request);
            
            GetExchangeTokensResponse response = new();

            Response<GetTokensResponse> serviceResponse = await _tokenService.Get(requestToken, cancellationToken);
            
            if (!serviceResponse.IsSuccess)
                return Response.WithError<GetExchangeTokensResponse>(serviceResponse.Error?.StatusCode);

            serviceResponse.Result?.Tokens.ForEach(t => response.AvailableExchanges.Add(t.Exchange, t.AvailableServices));

            return Response.Success(response);
        }
    }
}