using MediatR;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Models
{
    /// <summary>
    /// Model for <see cref="Handlers.GetAllPricesHandler"/> for getting symbols with their prices
    /// </summary>
    /// <remarks>
    /// Implements <see cref="IRequest{TResponse}"/> <br/>
    /// <c>TResponse</c> is <see cref="Results.SymbolPricesResult"/>
    /// </remarks>
    public class SymbolPricesModel: IRequest<Results.SymbolPricesResult>
    {
    }
}
