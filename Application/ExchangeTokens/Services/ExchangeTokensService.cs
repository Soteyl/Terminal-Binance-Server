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

        public ExchangeTokensService(IExchangeTokenRepository repository, ExchangesValidatorByToken validator,
            IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
            _validator = validator;
        }

        /// <summary>
        /// Gets tokens from repository and removes invalid tokens if exist
        /// </summary>
        // TODO redis cache is required
        public async Task<Response<IEnumerable<CheckedExchangeToken>>> GetTokensByUserId(string userId,
            CancellationToken cancellationToken = default)
        {
            List<CheckedExchangeToken> resultTokens = new();
            IEnumerable<ExchangeToken> tokens = await _repository.Get(userId, cancellationToken);

            foreach (ExchangeToken? token in tokens)
            {
                IEnumerable<string> list =
                    (await _validator.Validate(token.Key, token.Secret, token.Exchange)).ToArray();

                if (list.Any())
                {
                    CheckedExchangeToken checkedToken = _mapper.Map<CheckedExchangeToken>(token);
                    checkedToken.AvailableServices = list;
                    resultTokens.Add(checkedToken);
                }
                
                else await _repository.Remove(new RepositoryUserExchange
                {
                    UserId = userId,
                    Exchange = token.Exchange
                }, cancellationToken);
            }

            return Response.Success((IEnumerable<CheckedExchangeToken>)resultTokens);
        }

        public async Task<Response> AddToken(UserExchangeToken token, CancellationToken cancellationToken = default)
        {
            if ((await _validator.Validate(token.Key, token.Secret, token.Exchange)).Any() == false)
                return Response.WithError(ServerResponseCode.InvalidApiToken);

            await _repository.Add(token.UserId, _mapper.Map<ExchangeToken>(token));

            return Response.Success();
        }

        public async Task<Response> RemoveToken(UserExchange userExchange,
            CancellationToken cancellationToken = default)
        {
            IEnumerable<ExchangeToken> possibleTokens
                = await _repository.Get(userExchange.UserId, cancellationToken);

            if (!possibleTokens.Any(t => t.Exchange.Equals(userExchange.Exchange)))
            {
                return Response.WithError(ServerResponseCode.MissingApiToken);
            }

            await _repository.Remove(_mapper.Map<RepositoryUserExchange>(userExchange), cancellationToken);
            return Response.Success();
        }
    }
}