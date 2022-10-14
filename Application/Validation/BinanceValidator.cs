using Binance.Net;
using Binance.Net.Objects.Other;

using Ixcent.CryptoTerminal.StorageHandle;

namespace Ixcent.CryptoTerminal.Application.Validation
{
    /// <summary> Class that validates binance token by key and secret </summary>
    public class BinanceValidator : IExchangeValidator
    {
        /// <summary>
        /// Validates key and secret by accessing rights from a binance server
        /// </summary>
        /// <param name="key">Token key</param>
        /// <param name="secret">Token secret</param>
        /// <returns>A collection with accessed items names <c>(margin, spot, futures)</c></returns>
        public async Task<IEnumerable<string>> Validate(string key, string secret)
        {
            BinanceClient client = new();
            client.SetApiCredentials(key, secret);

            List<string> result = new();

            BinanceAPIKeyPermissions? data = (await client.General.GetAPIKeyPermissionsAsync()).Data;

            if (data == null)
                return result;

            if (data.EnableMargin)
                result.Add("margin");

            if (data.EnableFutures)
                result.Add("futures");

            if (data.EnableSpotAndMarginTrading)
            {
                result.Add("spot");
            }

            return result;
        }
    }

    /// <summary>
    /// Validates Binance token by userId. Gets information from a database.
    /// </summary>
    public class BinanceTokenValidator : ExchangeTokenValidatorChoosedExchange
    {
        public BinanceTokenValidator(CryptoTerminalContext context, string userId)
            : base(context, userId, "Binance")
        { }

        /// <summary>
        /// Add validating for spot access
        /// </summary>
        public BinanceTokenValidator Spot()
        {
            _requiredPoints.Add("spot");
            return this;
        }

        /// <summary>
        /// Add validating for futures access
        /// </summary>
        public BinanceTokenValidator Futures()
        {
            _requiredPoints.Add("futures");
            return this;
        }

        /// <summary>
        /// Add validating for margin access
        /// </summary>
        public BinanceTokenValidator Margin()
        {
            _requiredPoints.Add("margin");
            return this;
        }
    }
}