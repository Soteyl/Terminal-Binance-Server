using System.Security.Claims;

using Ixcent.CryptoTerminal.Domain.Common.Models;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Ixcent.CryptoTerminal.Api.Controllers
{
    /// <summary>
    /// Base controller with <see cref="IMediator"/> field to implement DI.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private IMediator? _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;

        protected ActionResult ToHttpResponse(Response response)
        {
            if (!response.IsSuccess)
                return BadRequest(response);
            
            return NoContent();
        }

        protected ActionResult ToHttpResponse<T>(Response<T> response)
        { 
            if (!response.IsSuccess)
                return BadRequest(response);
            
            return Ok(response);
        }
        

        protected string GetUserId()
            => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
