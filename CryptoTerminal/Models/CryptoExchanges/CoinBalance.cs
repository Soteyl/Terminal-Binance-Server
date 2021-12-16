namespace CryptoTerminal.Models.CryptoExchanges
{
    public class CoinBalance
    {
        private string _shortName;

        private string _longName;

        private decimal _amount;

        public CoinBalance(string shortName, string longName, decimal amount)
        {
            ShortName = shortName;
            LongName = longName;
            Amount = amount;
        }

        public decimal Amount { get => _amount; set => _amount = value; }

        public string LongName { get => _longName; set => _longName = value; }

        public string ShortName { get => _shortName; set => _shortName = value; }
    }
}
