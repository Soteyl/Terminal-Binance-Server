namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges
{
    public interface ICryptoExchange
    {
        List<CryptoFutures> GetFutures();

        CryptoSpot GetCryptoSpot();
    }
}
