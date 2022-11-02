using AutoMapper;

using Ixcent.CryptoTerminal.Application.Validation;
using Ixcent.CryptoTerminal.Domain.Common;
using Ixcent.CryptoTerminal.Domain.Common.Models;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Interfaces;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Repository;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Service;

using UserExchange = Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Service.UserExchange;
using RepositoryUserExchange = Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Repository.UserExchange;

namespace Ixcent.CryptoTerminal.Application.ExchangeTokens.Services
{
    public class ExchangeTokensService : IExchangeTokenService
    {
        private readonly IExchangeTokenRepository _repository;

        private readonly ExchangesValidatorByToken _validator;

        private readonly IMapper _mapper;

        public ExchangeTokensService(IExchangeTokenRepository repository, ExchangesValidatorByToken validator, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
            _validator = validator;
        }

        /// <summary>
        /// Gets tokens from repository and removes invalid tokens if exist
        /// </summary>
        // TODO redis cache is required
        public async Task<Response<IEnumerable<CheckedExchangeToken>>> Get(UserId userId, CancellationToken cancellationToken = default)
        {
            List<CheckedExchangeToken> resultTokens = new();

            var request = new GetTokensRequest
            {
                UserId = userId.Value
            };

            GetTokensResult result = await _repository.Get(request, cancellationToken);

            foreach (ExchangeToken? token in result.Tokens)
            {
                IEnumerable<string> list = (await _validator.Validate(token.Key, token.Secret, token.Exchange)).ToArray();

                if (list.Any())
                {
                    CheckedExchangeToken checkedToken = _mapper.Map<CheckedExchangeToken>(token);
                    checkedToken.AvailableServices = list;
                    resultTokens.Add(checkedToken);
                    continue;
                }
                
                var removeTokenRequest = new RemoveTokenRequest
                {
                    UserId = userId.Value,
                    Exchange = token.Exchange
                };
                await _repository.Remove(removeTokenRequest, cancellationToken);
            }
            return Response.Success((IEnumerable<CheckedExchangeToken>)resultTokens);
        }

        public async Task<Response> AddToken(UserExchangeToken token, CancellationToken cancellationToken = default)
        {
            if ((await _validator.Validate(token.Key, token.Secret, token.Exchange)).Any() == false)
                return Response.WithError(ServerResponseCode.InvalidApiToken);

            var addTokenRequest = new AddTokenRequest
            {
                UserId = token.UserId
            };

            await _repository.Add(_mapper.Map<AddTokenRequest>(addTokenRequest), cancellationToken);
            return Response.Success();
        }

        public async Task<Response> RemoveToken(UserExchange userExchange, CancellationToken cancellationToken = default)
        {
            var getTokenRequest = new GetTokensRequest
            {
                UserId = userExchange.UserId
            };

            GetTokensResult possibleTokens = await _repository.Get(getTokenRequest, cancellationToken);

            if (!possibleTokens.Tokens.Any(t => t.Exchange.Equals(userExchange.Exchange)))
            {
                return Response.WithError(ServerResponseCode.MissingApiToken);
            }

            var removeTokenRequest = new RemoveTokenRequest
            {
                UserId = userExchange.UserId,
                Exchange = userExchange.Exchange
            };

            await _repository.Remove(_mapper.Map<RemoveTokenRequest>(removeTokenRequest), cancellationToken);
            return Response.Success();
        }
    }
}