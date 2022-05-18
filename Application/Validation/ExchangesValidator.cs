using Ixcent.CryptoTerminal.Application.Exceptions;

namespace Ixcent.CryptoTerminal.Application.Validation
{
    public class ExchangesValidator
    {
        private Dictionary<string, IExchangeValidator> _exchangesValidator = new Dictionary<string, IExchangeValidator>(StringComparer.OrdinalIgnoreCase)
        {
            { "binance", new BinanceValidator() }
        };

        public async Task<List<string>> Validate(string token, string secret, string exchangeName)
        {

            if (!_exchangesValidator.Keys.Contains(exchangeName))
                throw new RestException(System.Net.HttpStatusCode.BadRequest, new
                {
                    Exchange = "Wrong exchange name!"
                });
            return await _exchangesValidator[exchangeName].Validate(token, secret);
        }

    }
}
