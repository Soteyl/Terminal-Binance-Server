using System.Net;

using Ixcent.CryptoTerminal.Application.Exceptions;
using Ixcent.CryptoTerminal.Application.Interfaces;
using Ixcent.CryptoTerminal.Domain.Database;

using MediatR;

using Microsoft.AspNetCore.Identity;

namespace Ixcent.CryptoTerminal.Application.Users.Login
{
    public class LoginHandler : IRequestHandler<LoginQuery, User>
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;

        private readonly IJwtGenerator _jwtGenerator;

        public LoginHandler(UserManager<AppUser> userManager,
                            SignInManager<AppUser> signInManager,
                            IJwtGenerator jwtGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<User> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            AppUser? user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.Unauthorized,
                                        ErrorCode.InvalidData,
                                        new { Message = "Invalid login/password" });
            }

            SignInResult? result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (result.Succeeded)
            {
                return new User
                {
                    Email = user.Email,
                    Token = _jwtGenerator.CreateToken(user),
                    UserName = user.UserName
                };
            }

            throw new RestException(HttpStatusCode.Unauthorized, ErrorCode.Unknown);
        }
    }
}
