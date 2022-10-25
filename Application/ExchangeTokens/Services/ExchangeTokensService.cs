using AutoMapper;

using Ixcent.CryptoTerminal.Application.Validation;
using Ixcent.CryptoTerminal.Domain.Common;
using Ixcent.CryptoTerminal.Domain.Common.Models;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Interfaces;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Repository;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Service;

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
        public async Task<Response<IEnumerable<CheckedExchangeToken>>> GetTokensByUserId(string userId)
        {
            List<CheckedExchangeToken> resultTokens = new();
            IEnumerable<ExchangeToken> tokens = await _repository.GetTokensByUserId(userId);

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
                else await _repository.RemoveToken(userId, token.Exchange);
            }

            return Response.Success((IEnumerable<CheckedExchangeToken>)resultTokens);
        }

        public async Task<Response> AddToken(UserExchangeToken token)
        {
            if ((await _validator.Validate(token.Key, token.Secret, token.Exchange)).Any() == false)
                return Response.WithError(ServerResponseCode.InvalidApiToken);

            await _repository.AddToken(token.UserId, _mapper.Map<ExchangeToken>(token));

            return Response.Success();
        }

        public async Task<Response> RemoveToken(UserExchange userExchange)
        {
            IEnumerable<ExchangeToken> possibleTokens 
                = await _repository.GetTokensByUserId(userExchange.UserId);
            
            if (!possibleTokens.Any(t => t.Exchange.Equals(userExchange.Exchange)))
            {
                return Response.WithError(ServerResponseCode.MissingApiToken);
            }

            await _repository.RemoveToken(userExchange.UserId, userExchange.Exchange);
            return Response.Success();
        }
    }
}