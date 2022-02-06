using CryptoExchange.Net.ExchangeInterfaces;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.Data
{
    public class CoinBalance: ICommonBalance
    {
        public decimal Free { get; set; }

        public decimal Locked { get; set;}

        public string Asset { get; set; }

        public decimal Total => Free + Locked;

        string ICommonBalance.CommonAsset => Asset;

        decimal ICommonBalance.CommonAvailable => Free;

        decimal ICommonBalance.CommonTotal => Total;
    }
}
