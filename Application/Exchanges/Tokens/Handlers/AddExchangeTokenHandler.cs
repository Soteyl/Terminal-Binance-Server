using System.Net;

using AutoMapper;

using Ixcent.CryptoTerminal.Application.Exceptions;
using Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Models;
using Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Services;
using Ixcent.CryptoTerminal.Application.Mediatr;
using Ixcent.CryptoTerminal.Application.Validation;
using Ixcent.CryptoTerminal.Domain.Database.Models;
using Ixcent.CryptoTerminal.StorageHandle;

using MediatR;

using Microsoft.AspNetCore.Http;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Handlers
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
