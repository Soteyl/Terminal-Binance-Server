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

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        CryptoTerminalContext _dataBase;

        public UsersController(CryptoTerminalContext context)
        {
            _dataBase = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _dataBase.Users.FirstOrDefaultAsync(u => u.UserName == model.UserName);
                if (user == null)
                {
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
            }

            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var identity = await GetIdentity(loginModel.UserNameOrPassword, loginModel.Password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
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
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role.ToString())
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}
