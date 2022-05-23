using Microsoft.AspNetCore.Mvc;

namespace Ixcent.CryptoTerminal.Api.Controllers
{
    using Application.Exchanges.Binance.Models;
    using Binance.Net.Objects.Spot.SpotData;

    /// <summary>
    /// Controller for Binance users. Requires authorization. <para/>
    /// Url: <c>api/binance/</c> <br/>
    /// Inherited from <see cref="BaseController"/> <br/>
    /// Contains <see cref="ApiControllerAttribute"/>, <see cref="RouteAttribute"/>
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BinanceController : BaseController
    {
        /// <summary>
        /// POST Url: <c>api/binance/order</c>
        /// </summary>
        /// <param name="command">spot order info</param>
        /// <returns><see cref="MakeSpotOrderResult"/> object</returns>
        [HttpPost("order")]
        public async Task<ActionResult<MakeSpotOrderResult>> MakeSpotOrderAsync(MakeSpotOrderModel command)
        {
            return await Mediator.Send(command);
        }

        /// <summary>
        /// GET Url: <c>api/binance/balance-spot</c>
        /// </summary>
        /// <returns> Collection of <see cref="BinanceBalance"/> </returns>
        [HttpGet("balance-spot")]
        public async Task<ActionResult<IEnumerable<BinanceBalance>>> GetAllBalancesAsync()
        {
            return Ok(await Mediator.Send(new GetAllBalancesSpotModel()));
        }
    }
}
