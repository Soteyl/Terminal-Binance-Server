using System.Net;

using Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Models;
using Ixcent.CryptoTerminal.Application.Mediatr;

using Microsoft.AspNetCore.Mvc;

namespace Ixcent.CryptoTerminal.Api.Controllers
{
    /// <summary>
    /// Controller for adding, editing, removing, getting exchange tokens by this template <c>{ key, secret }</c>
    /// </summary>
    /// <remarks>
    /// Url: <c>api/exchanges/</c> <br/>
    /// Inherited from <see cref="BaseController"/> <br/>
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    public class ExchangesController : BaseController
    {
        /// <summary> Adds or updates an exchange token into a server database </summary>
        /// <remarks> POST Url: <c>api/exchanges/token</c> </remarks>
        /// <param name="command">token key, secret, exchange name</param>
        /// <returns>error or Ok result</returns>
        /// 
        /// <response code="200">Returns available exchanges for this api token</response>
        /// <response code="400">
        /// Invalid user <br/>
        /// Specified api token already exists <br/> 
        /// Bad key or secret 
        /// </response>
        [HttpPost("token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Add(AddExchangeTokenQuery command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary> Deletes existing exchange access token from database </summary>
        /// <remarks> DELETE Url: <c>api/exchanges/token</c> </remarks>
        /// <param name="command">exchange name</param>
        /// <returns>error or Ok result</returns>
        /// 
        /// <response code="200">Removes token from a database</response>
        /// <response code="400">Couldn't find a token</response>
        [HttpDelete("token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(RemoveExchangeTokenQuery command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary> Gets description about all existing tokens inside a database. </summary>
        /// <remarks> GET Url: <c>api/exchanges/token</c> </remarks>
        /// <returns>Connected exchanges and their available functionality</returns>
        /// <response code="200"/>
        /// <response code="400"/>
        [HttpGet("token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<ExchangeTokensResult>>> GetTokens()
        {
            Response<ExchangeTokensResult> response =
                await Mediator.Send(new GetExchangeTokensQuery());
            
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }
    }
}