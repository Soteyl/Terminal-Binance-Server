namespace CryptoTerminal.Models.CryptoExchanges
{
    public class CoinBalance : ICloneable
    {
        private string _shortName;

        private decimal _free;

        private decimal _locked;

        /// <summary>
        /// Specifies a balance in a specific crypto currency.
        /// </summary>
        /// <param name="shortName">Symbol of a cryptocurrency.</param>
        /// <param name="longName">Full name of a cryptocurrency.</param>
        /// <param name="amount">Amount of a cryptocurrency.</param>
        public CoinBalance(string shortName, string longName, decimal amount)
        {
            ShortName = shortName;
            _free = free;
            _locked = locked;
        }
        /// <summary>
        /// Symbol of a cryptocurrency.
        /// </summary>
        public decimal Amount { get => _amount; set => _amount = value; }
        /// <summary>
        /// Full name of a cryptocurrency.
        /// </summary>
        public string LongName { get => _longName; set => _longName = value; }
        /// <summary>
        /// Amount of a cryptocurrency.
        /// </summary>
        public string ShortName { get => _shortName; set => _shortName = value; }
        
        public object Clone()
        {
            return new CoinBalance(ShortName, LongName, Amount);
        }
    }
}
