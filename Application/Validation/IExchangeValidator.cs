using Binance.Net;

namespace Ixcent.CryptoTerminal.Application.Validation
{
    internal interface IExchangeValidator
    {
        Task<List<string>> Validate(string token, string secret);
    }

    public class BinanceValidator : IExchangeValidator
    {
        public async Task<List<string>> Validate(string token, string secret)
        {
            BinanceClient client = new BinanceClient();
            client.SetApiCredentials(token, secret);

            List<string> result = new List<string>();

            var data = (await client.General.GetAPIKeyPermissionsAsync()).Data;

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

}