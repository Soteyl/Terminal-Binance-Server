namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges
{
    public abstract class CryptoFutures
    {
        private readonly string _mainCoin;

        public CryptoFutures(string mainCoin)
        {
            _mainCoin = mainCoin;
        }

        public string MainCoin => _mainCoin;
    }
}