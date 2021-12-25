namespace CryptoTerminal.Models.CryptoExchanges
{
    public enum TimeInForce
    {
        //
        // Сводка:
        //     GoodTillCancel orders will stay active until they are filled or canceled
        GoodTillCancel,
        //
        // Сводка:
        //     ImmediateOrCancel orders have to be at least partially filled upon placing or
        //     will be automatically canceled
        ImmediateOrCancel,
        //
        // Сводка:
        //     FillOrKill orders have to be entirely filled upon placing or will be automatically
        //     canceled
        FillOrKill,
        //
        // Сводка:
        //     GoodTillCrossing orders will post only
        GoodTillCrossing,
        //
        // Сводка:
        //     Good til the order expires or is canceled
        GoodTillExpiredOrCanceled
    }

    public static class TimeInForceExtensions
    {
        public static Binance.Net.Enums.TimeInForce ToBinanceTIF(this TimeInForce force)
        {
            return (Binance.Net.Enums.TimeInForce)(int)force;
        }
    }
}
