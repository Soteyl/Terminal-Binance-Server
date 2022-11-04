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
    /// <summary> Handler for updating exchange token into a database. </summary>
    /// <remarks>
    /// Implements <see cref="IRequestHandler{TRequest}"/> <br/>
    /// <c>TRequest</c> is <see cref="AddExchangeTokenQuery"/> <br/>
    /// </remarks>
    public class AddExchangeTokenHandler : IRequestHandlerBase<AddExchangeTokenQuery>
    {
        
        private readonly IExchangeTokenService _service;
        
        private readonly IMapper _mapper;
        
        private readonly IValidatorResolver _validator;

        public AddExchangeTokenHandler(IExchangeTokenService service,
                                       IMapper mapper,
                                       IValidatorResolver validatorResolver)
        {
            _service = service;
            _mapper = mapper;
            _validator = validatorResolver;
        }

        public async Task<Response> Handle(AddExchangeTokenQuery request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAsync(request, cancellationToken);

            AddTokenRequest tokenRequest = _mapper.Map<AddTokenRequest>(request);
            tokenRequest.UserId = request.UserId;

            Response response = await _service.Add(tokenRequest, cancellationToken);
            
            return response;
        }
    }
}
