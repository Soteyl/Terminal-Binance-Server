using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ixcent.CryptoTerminal.Api.Controllers
{
    using Application.Exchanges.Binance.Models;

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
        /// <param name="model">spot order info</param>
        /// <returns><see cref="MakeSpotOrderResult"/> object</returns>
        [HttpPost("order")]
        public async Task<ActionResult<MakeSpotOrderResult>> MakeSpotOrderAsync(MakeSpotOrderModel command)
        {
            return await Mediator.Send(command);
        }
    }
}
