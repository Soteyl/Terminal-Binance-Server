using Binance.Net.Objects.Futures.FuturesData;

namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.Data
{
    /// <summary>
    /// Class is based on <see cref="BinanceFuturesAccountBalance"/>
    /// </summary>
    public class FuturesBalance
    {
        public string AccountAlias
        {
            get;
            set;
        } = string.Empty;

        public string Asset
        {
            get;
            set;
        } = string.Empty;

        public decimal WalletBalance
        {
            get;
            set;
        }

        public decimal CrossWalletBalance
        {
            get;
            set;
        }

        public decimal CrossUnrealizedPnl
        {
            get;
            set;
        }

        public decimal AvailableBalance
        {
            get;
            set;
        }

        public decimal MaxWithdrawAmount
        {
            get;
            set;
        }

        public bool? MarginAvailable
        {
            get;
            set;
        }

        public static implicit operator FuturesBalance(BinanceFuturesAccountBalance bal)
        {
            return new FuturesBalance
            {
                AccountAlias = bal.AccountAlias,
                Asset = bal.Asset,
                WalletBalance = bal.WalletBalance,
                AvailableBalance = bal.AvailableBalance,
                CrossUnrealizedPnl = bal.CrossUnrealizedPnl,
                CrossWalletBalance = bal.CrossWalletBalance,
                MarginAvailable = bal.MarginAvailable,
                MaxWithdrawAmount = bal.MaxWithdrawAmount
            };
        }

        public static implicit operator BinanceFuturesAccountBalance(FuturesBalance bal)
        {
            // TODO ТЕСТИМ ПРИКОЛ С ПРИВЕДЕНИЕМ ТИПА 👻👻👻
            return bal;
        }
    }
}
