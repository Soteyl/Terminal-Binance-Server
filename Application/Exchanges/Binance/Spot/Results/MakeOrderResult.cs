namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Results
{
    /// <summary>
    /// Result of making Binance spot orders
    /// </summary>
    public class MakeOrderResult
    {
        public bool HasPlacedOrder { get; set; }

        public string? Message { get; set; }
    }
}
