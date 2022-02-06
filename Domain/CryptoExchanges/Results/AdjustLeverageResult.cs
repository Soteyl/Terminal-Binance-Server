using Binance.Net.Objects.Futures.FuturesData;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.Results
{
    public class AdjustLeverageResult
    {
        public int Leverage
        {
            get;
            set;
        }
        public string MaxNotionalValue
        {
            get;
            set;
        } = string.Empty;

        public string? Symbol
        {
            get;
            set;
        }

        public static implicit operator AdjustLeverageResult(BinanceFuturesInitialLeverageChangeResult res)
        {
            return new AdjustLeverageResult
            {
                Leverage = res.Leverage,
                MaxNotionalValue = res.MaxNotionalValue,
                Symbol = res.Symbol
            };
        }
    }
}