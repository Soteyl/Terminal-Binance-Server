using AutoMapper;

using Ixcent.CryptoTerminal.Application.Validation;
using Ixcent.CryptoTerminal.Domain.Common;
using Ixcent.CryptoTerminal.Domain.Common.Interfaces;
using Ixcent.CryptoTerminal.Domain.Common.Models;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Interfaces;

using Repository = Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Repository;
using Service = Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Service;

namespace Ixcent.CryptoTerminal.Application.ExchangeTokens.Services
{
    public class ExchangeTokensService : IExchangeTokenService
    {
        private readonly IExchangeTokenRepository _repository;

        private readonly ExchangesValidatorByToken _validator;
        private readonly IValidatorResolver _validatorResolver;

        private readonly IMapper _mapper;

        public ExchangeTokensService(IExchangeTokenRepository repository, ExchangesValidatorByToken validator, IValidatorResolver validatorResolver,
            IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
            _validator = validator;
            _validatorResolver = validatorResolver;
        }

        /// <summary>
        /// Gets tokens from repository and removes invalid tokens if exist
        /// </summary>
        // TODO redis cache is required
        public async Task<Response<Service.GetTokensResponse>> Get(Service.GetExchangeTokensRequest getExchangeTokensRequest,
            CancellationToken cancellationToken = default)
        {
            await _validatorResolver.ValidateAsync(getExchangeTokensRequest, cancellationToken);
            
            List<Service.CheckedExchangeToken> resultTokens = new();

            var request = new Repository.GetTokensRequest { UserId = getExchangeTokensRequest.UserId };

            Repository.GetTokensResult result = await _repository.Get(request, cancellationToken);

            foreach (Repository.ExchangeToken? token in result.Tokens)
            {
                IEnumerable<string> list =
                    (await _validator.Validate(token.Key, token.Secret, token.Exchange)).ToArray();

                if (list.Any())
                {
                    Service.CheckedExchangeToken checkedToken = _mapper.Map<Service.CheckedExchangeToken>(token);
                    checkedToken.AvailableServices = list;
                    resultTokens.Add(checkedToken);
                    continue;
                }

                var removeTokenRequest = new Repository.RemoveTokenRequest
                {
                    UserId = getExchangeTokensRequest.UserId, Exchange = token.Exchange
                };
                await _repository.Remove(removeTokenRequest, cancellationToken);
            }

            return Response.Success(new Service.GetTokensResponse { Tokens = resultTokens });
        }

        public async Task<Response> Add(Service.AddTokenRequest tokenRequest,
            CancellationToken cancellationToken = default)
        {
            await _validatorResolver.ValidateAsync(tokenRequest, cancellationToken);
            
            if ((await _validator.Validate(tokenRequest.Key, tokenRequest.Secret, tokenRequest.Exchange)).Any() ==
                false)
                return Response.WithError(ServerResponseCode.InvalidApiToken);

            var addTokenRequest = new Repository.AddTokenRequest { UserId = tokenRequest.UserId };

            await _repository.Add(_mapper.Map<Repository.AddTokenRequest>(addTokenRequest), cancellationToken);
            return Response.Success();
        }

        public async Task<Response> Remove(Service.RemoveTokenRequest removeTokenRequest,
            CancellationToken cancellationToken = default)
        {
            await _validatorResolver.ValidateAsync(removeTokenRequest, cancellationToken);
            
            var getTokenRequest = new Repository.GetTokensRequest { UserId = removeTokenRequest.UserId };

            Repository.GetTokensResult possibleTokens = await _repository.Get(getTokenRequest, cancellationToken);

            if (!possibleTokens.Tokens.Any(t => t.Exchange.Equals(removeTokenRequest.Exchange)))
            {
                return Response.WithError(ServerResponseCode.MissingApiToken);
            }

            await _repository.Remove(_mapper.Map<Repository.RemoveTokenRequest>(removeTokenRequest), cancellationToken);
            return Response.Success();
        }
    }
}