namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges
{
    public enum OrderSide
    {
        Buy,
        Sell
    }

    public static class OrderSideExtensions
    {
        public static Binance.Net.Enums.OrderSide ConvertToBinanceOrderSide(this OrderSide side)
        {
            return (side == OrderSide.Buy) ? Binance.Net.Enums.OrderSide.Buy : Binance.Net.Enums.OrderSide.Sell;
        }
    }
}
