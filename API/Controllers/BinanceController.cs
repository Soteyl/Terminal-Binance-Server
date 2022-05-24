using Microsoft.AspNetCore.Mvc;

namespace Ixcent.CryptoTerminal.Api.Controllers
{
    using Application.Exchanges.Binance.Models;

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
        [HttpPost("order")]
        public async Task<ActionResult<MakeSpotOrderResult>> MakeSpotOrderAsync(MakeSpotOrderModel command)
        {
            return await Mediator.Send(command);
        }
    }
}
