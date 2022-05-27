using Microsoft.EntityFrameworkCore;

namespace Ixcent.CryptoTerminal.Application.Validation
{
    using Exceptions;

    /// <summary>
    /// Complex validator for all exchange tokens.
    /// </summary>
    public class ExchangesValidatorByToken
    {
        private readonly Dictionary<string, IExchangeValidator> _exchangesValidators =
            new Dictionary<string, IExchangeValidator>(StringComparer.OrdinalIgnoreCase)
            {
                { "Binance", new BinanceValidator() }
            };

        /// <summary>
        /// Chooses specific validator by an exchange name and validates token
        /// </summary>
        /// <param name="key">Token key</param>
        /// <param name="secret">Token secret</param>
        /// <param name="exchangeName">Name of exchange</param>
        /// <returns>A collection with names of accessed items</returns>
        /// <exception cref="RestException"></exception>
        public async Task<IEnumerable<string>> Validate(string key, string secret, string exchangeName)
        {

            if (!_exchangesValidators.Keys.Contains(exchangeName))
                throw new RestException(System.Net.HttpStatusCode.BadRequest, ErrorCode.NotFound, new
                {
                    Exchange = "Wrong exchange name!"
                });
            return await _exchangesValidators[exchangeName].Validate(key, secret);
        }

        /// <summary>
        /// Gets specific validator by name
        /// </summary>
        /// <param name="exchangeName">Name of exchange</param>
        /// <returns>Needed validator or null if there is no a validator</returns>
        public IExchangeValidator GetValidatorByName(string exchangeName)
        {
            _exchangesValidators.TryGetValue(exchangeName, out IExchangeValidator validator);
            return validator;
        }

        /// <summary>
        /// Get Binance validator
        /// </summary>
        public BinanceValidator Binance()
        {
            return new BinanceValidator();
        }
    }
}
