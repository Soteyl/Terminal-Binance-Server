using AutoMapper;
using Ixcent.CryptoTerminal.Domain.Common.Models;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Handler;
using Microsoft.AspNetCore.Mvc;
using Controller = Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Controller;

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
        private readonly IMapper _mapper;

        public ExchangesController(IMapper mapper)
        {
            _mapper = mapper;
        }
        
        
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
        public async Task<ActionResult> Add(Controller.AddExchangeTokenQuery command)
        {
            var query = _mapper.Map<AddExchangeTokenQuery>(command);
            query.UserId = GetUserId();
            
            await Mediator.Send(query);
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
        public async Task<ActionResult> Delete(Controller.RemoveExchangeTokenQuery command)
        {
            var query = _mapper.Map<RemoveExchangeTokenQuery>(command);
            query.UserId = GetUserId();
            
            await Mediator.Send(query);
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
        public async Task<ActionResult<Response<GetExchangeTokensResponse>>> GetTokens()
        {
            var query = _mapper.Map<GetExchangeTokensQuery>(new Controller.GetExchangeTokensQuery());
            query.UserId = GetUserId();
            Response<GetExchangeTokensResponse> response = await Mediator.Send(query);
            
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }
    }
}