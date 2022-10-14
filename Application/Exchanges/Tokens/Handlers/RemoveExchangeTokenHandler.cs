﻿using Ixcent.CryptoTerminal.Application.Exceptions;
using Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Models;
using Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Services;
using Ixcent.CryptoTerminal.Application.Mediatr;
using Ixcent.CryptoTerminal.Application.Status;
using Ixcent.CryptoTerminal.Domain.Database.Models;
using Ixcent.CryptoTerminal.StorageHandle;

using MediatR;

using Microsoft.AspNetCore.Http;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Handlers
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

            Response response =
                await _service.RemoveToken(new UserExchange { Exchange = request.Exchange, UserId = userId });

            return response;
        }
    }
}