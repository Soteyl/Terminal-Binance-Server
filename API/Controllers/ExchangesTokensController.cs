using Microsoft.AspNetCore.Mvc;

namespace Ixcent.CryptoTerminal.Api.Controllers
{
    using Application.Exchanges.Tokens.Models;

    [ApiController]
    [Route("api/[controller]")]
    public class ExchangesTokensController : BaseController
    {
        [HttpPost("token")]
        public async Task<ActionResult> Add(AddExchangeTokenQuery command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        [HttpPut("token")]
        public async Task<ActionResult> Edit(UpdateExchangeTokenQuery command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        [HttpDelete("token")]
        public async Task<ActionResult> Delete(RemoveExchangeTokenQuery command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        [HttpGet("token")]
        public async Task<ActionResult<ExchangeTokensResult>> GetTokens(GetExchangeTokensQuery command)
        {
            return await Mediator.Send(command);
        }

    }
}
