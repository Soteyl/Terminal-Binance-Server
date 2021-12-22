using Microsoft.AspNetCore.Mvc;

namespace CryptoTerminal.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using Models.Database;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Models.Auth;

    /// <summary>
    /// Controller for registration and login <para/>
    /// Url: <c>api/users/</c>
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
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
        /// <returns><see cref="OkObjectResult"/> or <see cref="BadRequestObjectResult"/></returns>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            User user = await _dataBase.Users.FirstOrDefaultAsync(u => u.UserName == model.UserName || u.Email == model.Email);

            if (user != null) return BadRequest(new { errorText = "Пользователь с такими данными уже существует." });

            user = new User
            {
                Email = model.Email,
                UserName = model.UserName,
                Password = model.Password,
                RoleId = _dataBase.GetUserRole("user").Id
            };

            _dataBase.Users.Add(user);
            await _dataBase.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Login user. <para/>
        /// Url: <c>api/users/login</c>
        /// </summary>
        /// <param name="loginModel">Login info</param>
        /// <returns>Json with access_token and username or <see cref="BadRequestObjectResult"/></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            ClaimsIdentity? identity = await GetIdentity(loginModel.UserNameOrEmail, loginModel.Password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            // create JWT-token
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.Issuer,
                    audience: AuthOptions.Audience,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(AuthOptions.LifeTime),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Json(response);
        }

        private async Task<ClaimsIdentity> GetIdentity(string usernameOrEmail, string password)
        {
            User person = await _dataBase.Users.FirstOrDefaultAsync(user => (user.UserName == usernameOrEmail || user.Email.Equals(usernameOrEmail)) && user.Password == password);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.UserName),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, _dataBase.GetUserRole(person.RoleId).Name)
                };

                var claimsIdentity = new ClaimsIdentity(claims,
                                                        authenticationType: "Token",
                                                        nameType: ClaimsIdentity.DefaultNameClaimType,
                                                        roleType: ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }

            // if user missing
            return null;
        }
    }
}
