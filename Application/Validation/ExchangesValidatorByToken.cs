using Ixcent.CryptoTerminal.Application.Exceptions;
using Ixcent.CryptoTerminal.Domain.Common;

namespace Ixcent.CryptoTerminal.Application.Validation
{
    /// <summary>
    /// Complex validator for all exchange tokens.
    /// </summary>
    public class ExchangesValidatorByToken
    {
        private readonly Dictionary<string, IExchangeValidator> _exchangesValidators =
            new(StringComparer.OrdinalIgnoreCase)
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
        /// <exception cref="ServerException"></exception>
        public async Task<IEnumerable<string>> Validate(string key, string secret, string exchangeName)
        {

            if (!_exchangesValidators.ContainsKey(exchangeName))
                throw new ServerException(ServerResponseCode.MissingApiToken, "Wrong exchange name!");
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
