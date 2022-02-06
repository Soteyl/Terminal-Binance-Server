using Binance.Net.Objects.Futures.FuturesData;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.Results
{
    public class ChangeMarginTypeResult
    {
        public int Code { get; set; }

        public string? Message { get; set; }

        public static implicit operator ChangeMarginTypeResult(BinanceFuturesChangeMarginTypeResult res)
        {
            return new ChangeMarginTypeResult
            {
                Code = res.Code,
                Message = res.Message
            };
        }
    }
}
