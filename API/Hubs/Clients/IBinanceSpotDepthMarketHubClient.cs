using Binance.Net.Interfaces;

namespace Ixcent.CryptoTerminal.Api.Hubs.Clients
{
    /// <summary>
    /// Client for <see cref="BinanceSpotDepthMarketHub"/> 
    /// </summary>
    public interface IBinanceSpotDepthMarketHubClient
    {
        /// <summary> Receive Binance spot depth market updates </summary>
        /// <param name="orderBook">Bids and asks from Binance</param>
        Task ReceiveDepthMarketUpdate(IBinanceOrderBook orderBook);
    }
}
