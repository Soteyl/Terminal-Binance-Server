using Binance.Net.Objects.Spot.SpotData;
using Microsoft.AspNetCore.Mvc;

namespace Ixcent.CryptoTerminal.Api.Controllers
{
    using Application.Exchanges.Binance.Spot.Models;
    using Application.Exchanges.Binance.Spot.Results;

    /// <summary>
    /// Controller for Binance users. Requires authorization.
    /// </summary>
    /// <remarks>
    /// Url: <c>api/binance/</c> <br/>
    /// Inherited from <see cref="BaseController"/> <br/>
    /// Contains <see cref="ApiControllerAttribute"/>, <see cref="RouteAttribute"/>
    /// </remarks>
    [ApiController]
    [Route("api/binance/spot")]
    public class BinanceSpotController : BaseController
    {
        /// <summary> Makes Binance spot order </summary>
        /// <remarks> POST Url: <c>api/binance/order</c> </remarks>
        /// <param name="command">spot order info</param>
        /// <returns><see cref="MakeOrderResult"/> object</returns>
        /// <response code="200"> Returns placed order result. </response>
        /// <response code="400"> Bad API token</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("order")]
        public async Task<ActionResult<MakeOrderResult>> MakeSpotOrderAsync(MakeOrderModel command)
        {
            return await Mediator.Send(command);
        }

        /// <summary> Gets account balances </summary>
        /// <remarks> GET Url: <c>api/binance/spot/balance</c> </remarks>
        /// <returns> Collection of <see cref="BinanceBalance"/> </returns>
        /// <response code="200">Returns all the balances in the binance exchange for current user.</response>
        /// <response code="400">
        /// API exchange token is outdated. <br/>
        /// No token was found. <br/>
        /// </response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("balance")]
        public async Task<ActionResult<GetAllBalancesResult>> GetAllBalancesSpotAsync()
        {
            return await Mediator.Send(new GetAllBalancesModel());
        }

        /// <summary> Gets all symbols and their prices </summary>
        /// <remarks> GET Url: <c>api/binance/spot/prices</c></remarks>
        /// <returns> Collection of <see cref="Binance.Net.Objects.Spot.MarketData.BinancePrice"/></returns>
        /// <response code="200"/>
        [HttpGet("prices")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<SymbolPricesResult>> GetAllPrices()
        {
            return await Mediator.Send(new SymbolPricesModel());
        }

        /// <summary> Gets all open orders for current user </summary>
        /// <remarks> GET Url: <c>api/binance/spot/open-orders</c></remarks>
        /// <returns>Collection of <see cref="BinanceOrder"/></returns>
        /// <response code="200/>
        [HttpGet("open-orders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<OpenOrdersResult>> GetOpenOrders()
        {
            return await Mediator.Send(new OpenOrdersModel());
        }
    }
}
