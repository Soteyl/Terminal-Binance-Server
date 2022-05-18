using Microsoft.AspNetCore.Mvc;

namespace Ixcent.CryptoTerminal.Api.Controllers
{
    using Application.Exchanges.Tokens.Models;

    /// <summary>
    /// Controller for adding, editing, removing, getting exchange tokens by this template { key, secret } <para/>
    /// Url: <c>api/exchanges/</c> <br/>
    /// Inherited from <see cref="BaseController"/> <br/>
    /// Contains <see cref="ApiControllerAttribute"/>, <see cref="RouteAttribute"/>
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ExchangesController : BaseController
    {
        /// <summary>
        /// POST Url: <c>api/exchanges/token</c>
        /// </summary>
        /// <param name="command">token key, secret, exchange name</param>
        /// <returns>error or Ok result</returns>
        [HttpPost("token")]
        public async Task<ActionResult> Add(AddExchangeTokenQuery command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// PUT Url: <c>api/exchanges/token</c>
        /// </summary>
        /// <param name="command">token key, secret, exchange name</param>
        /// <returns>error or Ok result</returns>
        [HttpPut("token")]
        public async Task<ActionResult> Edit(UpdateExchangeTokenQuery command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// DELETE Url: <c>api/exchanges/token</c>
        /// </summary>
        /// <param name="command">exchange name</param>
        /// <returns>error or Ok result</returns>
        [HttpDelete("token")]
        public async Task<ActionResult> Delete(RemoveExchangeTokenQuery command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// GET Url: <c>api/exchanges/token</c>
        /// </summary>
        /// <returns>Connected exchanges and their available functionality</returns>
        [HttpGet("token")]
        public async Task<ActionResult<ExchangeTokensResult>> GetTokens()
        {
            return await Mediator.Send(new GetExchangeTokensQuery());
        }

    }
}
