using Microsoft.AspNetCore.Mvc;

namespace Ixcent.CryptoTerminal.Api.Controllers
{
    using Application.Users;
    using Application.Users.Login;
    using Application.Users.Registration;
    using Microsoft.AspNetCore.Authorization;
    using System.Threading.Tasks;

    /// <summary> Controller for registration and login </summary>
    /// <remarks>
    /// Url: <c>api/users/</c> <br/>
    /// Inherited from <see cref="BaseController"/> <br/>
    /// Contains <see cref="ApiControllerAttribute"/>, <see cref="RouteAttribute"/>, <see cref="AllowAnonymousAttribute"/>
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class UsersController : BaseController
    {
        /// <summary> Register user </summary>
        /// <remarks> POST Url: <c>api/users/register</c> </remarks>
        /// <param name="command">Registration info</param>
        /// <returns><see cref="User"/> or <see cref="BadRequestObjectResult"/></returns>
        /// 
        /// <response code="200">Sucessful register</response>
        /// <response code="400">
        /// Email already exists <br/>
        /// Username already exists
        /// </response>
        /// <response code="500">Client creation failed</response>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> RegisterAsync(RegistrationCommand command)
        {
            return await Mediator.Send(command);
        }

        /// <summary> Login user </summary>
        /// <remarks> POST Url: <c>api/users/login</c> </remarks>
        /// <param name="command">Login info</param>
        /// <returns><see cref="User"/> or <see cref="UnauthorizedObjectResult"/></returns>
        /// 
        /// <response code="200">Successful login</response>
        /// <response code="401">Invalid login/password</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<User>> LoginAsync(LoginQuery command)
        {
            return await Mediator.Send(command);
        }
    }
}
