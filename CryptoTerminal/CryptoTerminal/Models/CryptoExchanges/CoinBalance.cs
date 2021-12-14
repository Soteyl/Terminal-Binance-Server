namespace CryptoTerminal.Models.CryptoExchanges
{
    public class CoinBalance
    {
        private string _shortName;

        private string _longName;

        private decimal _amount;

        public CoinBalance(string shortName, string longName, decimal amount)
        {
            _shortName = shortName;
            _longName = longName;
            _amount = amount;
        }

        public string ShortName => _shortName;

        public string LongName => _longName;

        public decimal Amount => _amount;
    }
}
