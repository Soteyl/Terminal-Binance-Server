using MediatR;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Models
{
    /// <summary>
    /// Request model for getting open Binance spot orders. <para/>
    /// 
    /// Implements <see cref="IRequest{TResponse}"/> whose generic type is <see cref="Results.OpenOrdersResult"/>
    /// </summary>
    public class OpenOrdersModel : IRequest<Results.OpenOrdersResult>
    {
    }
}
