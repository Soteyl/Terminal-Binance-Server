namespace CryptoTerminal.Models.CryptoExchanges
{
    public class CoinBalance
    {
        private string _shortName;

        private decimal _free;

        private decimal _locked;

        public CoinBalance(string shortName, decimal free, decimal locked)
        {
            ShortName = shortName;
            _free = free;
            _locked = locked;
        }

        public decimal Free { get => _free; set => _free = value; }

        public decimal Locked { get => _locked; set => _locked = value;}

        public decimal Total => Free + Locked;

        public string ShortName { get => _shortName; set => _shortName = value; }
    }
}
