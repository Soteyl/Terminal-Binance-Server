using Microsoft.AspNetCore.Mvc;

namespace Ixcent.CryptoTerminal.Api.Controllers
{
    using Domain.Database.Models;
    using EFData;
    [ApiController]
    [Route("api/[controller]")]
    public class ExchangesTokensController : BaseController
    {
        private CryptoTerminalContext _context;

        public ExchangesTokensController(CryptoTerminalContext context)
        {
            _context = context;
        }

        [Route("add")]
        public async Task<ActionResult> AddToken(ExchangeToken model)
        {
            _context.ExchangeTokens.Add(model);
            return Ok();
        }

        [Route("edit")]
        public async Task<ActionResult> EditToken(ExchangeToken model)
        {
            _context.Update(model);
            return Ok();
        }

        [Route("remove")]
        public async Task<ActionResult> RemoveToken(ExchangeToken model)
        {
            _context.Remove(model);
            return Ok();
        }        
    }
}
