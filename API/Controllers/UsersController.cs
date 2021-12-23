using Microsoft.AspNetCore.Mvc;

namespace Ixcent.CryptoTerminal.API.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using System.Threading.Tasks;
    using Application.Users.Login;
    using EFData;
    using Application.Users;
    using Application.Users.Registration;

    /// <summary>
    /// Controller for registration and login <para/>
    /// Url: <c>api/users/</c>
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class UsersController : BaseController
    {
        CryptoTerminalContext _dataBase;

        public UsersController(CryptoTerminalContext context)
        {
            _dataBase = context;
        }

        /// <summary>
        /// Register user. <para/>
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
        /// Login user. <para/>
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
