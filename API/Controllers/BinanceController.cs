using Microsoft.AspNetCore.Mvc;

namespace Ixcent.CryptoTerminal.Api.Controllers
{
    using Application.Exchanges.Binance.Models;
    using Binance.Net.Objects.Spot.SpotData;

    /// <summary>
    /// Controller for Binance users. Requires authorization.
    /// </summary>
    /// <remarks>
    /// Url: <c>api/binance/</c> <br/>
    /// Inherited from <see cref="BaseController"/> <br/>
    /// Contains <see cref="ApiControllerAttribute"/>, <see cref="RouteAttribute"/>
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    public class BinanceController : BaseController
    {
        /// <summary> Makes Binance spot order </summary>
        /// <remarks> POST Url: <c>api/binance/order</c> </remarks>
        /// <param name="command">spot order info</param>
        /// <returns><see cref="MakeSpotOrderResult"/> object</returns>
        [HttpPost("spot/order")]
        public async Task<ActionResult<MakeSpotOrderResult>> MakeSpotOrderAsync(MakeSpotOrderModel command)
        {
            return await Mediator.Send(command);
        }

        /// <summary>
        /// GET Url: <c>api/binance/balance-spot</c>
        /// </summary>
        /// <returns> Collection of <see cref="BinanceBalance"/> </returns>
        /// <response code="200">Returns all the balances in the binance exchange for current user.</response>
        /// <response code="400">
        /// API exchange token is outdated. <br/>
        /// No token was found. <br/>
        /// </response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("spot/balance")]
        public async Task<ActionResult<GetAllBalancesSpotResult>> GetAllBalancesSpotAsync()
        {
            return await Mediator.Send(new GetAllBalancesSpotModel());
        }
    }
}
