using Microsoft.AspNetCore.Mvc;

namespace Ixcent.CryptoTerminal.API.Controllers
{
    using Application.Users;
    using Application.Users.Login;
    using Application.Users.Registration;
    using Microsoft.AspNetCore.Authorization;
    using System.Threading.Tasks;

    /// <summary>
    /// Controller for registration and login <para/>
    /// Url: <c>api/users/</c> <br/>
    /// Inherited from <see cref="BaseController"/> <br/>
    /// Contains <see cref="ApiControllerAttribute"/>, <see cref="RouteAttribute"/>, <see cref="AllowAnonymousAttribute"/>
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class UsersController : BaseController
    {
        /// <summary>
        /// POST Register user. <para/>
        /// Url: <c>api/users/register</c>
        /// </summary>
        /// <param name="model">Registration info</param>
        /// <returns><see cref="User"/> or <see cref="BadRequestObjectResult"/></returns>
        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterAsync(RegistrationCommand command)
        {
            return await Mediator.Send(command);
        }

        /// <summary>
        /// POST Login user. <para/>
        /// Url: <c>api/users/login</c>
        /// </summary>
        /// <param name="loginModel">Login info</param>
        /// <returns><see cref="User"/> or <see cref="BadRequestObjectResult"/></returns>
        [HttpPost("login")]
        public async Task<ActionResult<User>> LoginAsync(LoginQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
