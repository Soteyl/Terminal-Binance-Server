﻿
using Ixcent.CryptoTerminal.Application.Exceptions;
using Ixcent.CryptoTerminal.Domain.Database.Models;
using Ixcent.CryptoTerminal.EFData;

namespace Ixcent.CryptoTerminal.Application.Validation
{
    /// <summary>
    /// Next factory entity for validating exchange tokens by user id with choosed exchange
    /// </summary>
    /// <remarks>Inherited from <see cref="ExchangesValidatorByUser"/></remarks>
    public class ExchangeTokenValidatorChoosedExchange : ExchangesValidatorByUser
    {
        protected string _exchangeName;

        protected IExchangeValidator _tokenValidator;

        public string ExchangeName => _exchangeName;

        internal ExchangeTokenValidatorChoosedExchange(CryptoTerminalContext context,
                                                       string userId,
                                                       string exchangeName) :
            base(context, userId)
        {
            _exchangeName = exchangeName;
            _tokenValidator = new ExchangesValidatorByToken().GetValidatorByName(exchangeName);

            if (_tokenValidator == null)
                throw new InvalidOperationException("There is no exchange with name " + exchangeName);
        }

        /// <summary>
        /// Adds a name which access need to validate
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public ExchangeTokenValidatorChoosedExchange AddRequiredPoint(string point)
        {
            _requiredPoints.Add(point);
            return this;
        }

        /// <summary>
        /// Calls <see cref="Validate"/> method and if it is not success, throws a <see cref="RestException"/> instance
        /// </summary>
        /// <exception cref="RestException"></exception>
        public async Task<TokenValidationResult> ValidateOrThrowRest()
        {
            var result = await Validate();
            if (result.IsSuccess == false)
            {
                throw new RestException(System.Net.HttpStatusCode.BadRequest,
                                        ErrorCode.BadExchangeToken,
                                        result.Errors);
            }
            return result;
        }

        /// <summary>
        /// Validates token
        /// </summary>
        public async Task<TokenValidationResult> Validate()
        {
            ExchangeToken? token = _context.ExchangeTokens
                .FirstOrDefault(t => t.Exchange == ExchangeName && t.UserId == _userId);

            if (token == null)
                return TokenValidationResult.Error(token,
                    new
                    {
                        Token = "Failed to get API token for specified parameters."
                    });

            IEnumerable<string>? availablePoints = await _tokenValidator.Validate(token.Key, token.Secret);

            foreach (var point in _requiredPoints)
            {
                if (availablePoints.Contains(point) == false)
                    return TokenValidationResult.Error(token, new
                    {
                        Token = "Failed validation"
                    });
            }

            return TokenValidationResult.Success(token);
        }
    }
}