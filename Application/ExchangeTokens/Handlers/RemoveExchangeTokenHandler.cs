using AutoMapper;

using Ixcent.CryptoTerminal.Domain.Common.Interfaces;
using Ixcent.CryptoTerminal.Domain.Common.Models;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Interfaces;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Handler;
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

        private readonly IExchangeTokenService _service;
        
        private readonly IMapper _mapper;
        
        private readonly IValidatorResolver _validator;

        public RemoveExchangeTokenHandler(IExchangeTokenService service, IMapper mapper, IValidatorResolver validatorResolver)
        {
            _service = service;
            _mapper = mapper;
            _validator = validatorResolver;
        }

        public async Task<Response> Handle(RemoveExchangeTokenQuery request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAsync(request, cancellationToken);

            RemoveTokenRequest tokenRequest = _mapper.Map<RemoveTokenRequest>(request);

            Response response = await _service.Remove(tokenRequest, cancellationToken);

            return response;
        }
    }
}