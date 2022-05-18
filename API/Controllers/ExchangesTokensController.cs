using Microsoft.AspNetCore.Mvc;

namespace Ixcent.CryptoTerminal.Api.Controllers
{
    using Application.Exchanges.Tokens.Models;
    using Microsoft.AspNetCore.Authorization;

    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class ExchangesTokensController : BaseController
    {
        [HttpPost("token")]
        public async Task<ActionResult<ExchangeTokenResult>> Add(AddExchangeTokenCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("token")]
        public async Task<ActionResult<ExchangeTokenResult>> Edit(UpdateExchangeTokenCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("token")]
        public async Task<ActionResult<ExchangeTokenResult>> Delete(RemoveExchangeTokenCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
