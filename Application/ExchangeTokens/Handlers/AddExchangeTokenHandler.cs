using AutoMapper;

using Ixcent.CryptoTerminal.Domain.Common.Interfaces;
using Ixcent.CryptoTerminal.Domain.Common.Models;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Interfaces;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Contracts;
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
        private readonly IHttpContextAccessor _contextAccessor;
        
        private readonly IExchangeTokenService _service;
        
        private readonly IMapper _mapper;

        public AddExchangeTokenHandler(IHttpContextAccessor contextAccessor,
                                       IExchangeTokenService service,
                                       IMapper mapper)
        {
            _contextAccessor = contextAccessor;
            _service = service;
            _mapper = mapper;
        }

        public async Task<Response> Handle(AddExchangeTokenQuery request, CancellationToken cancellationToken)
        {
            string currentUserId = _contextAccessor.GetCurrentUserId();

            UserExchangeToken token = _mapper.Map<UserExchangeToken>(request);
            token.UserId = currentUserId;

            Response response = await _service.AddToken(token);
            
            return response;
        }
    }
}
