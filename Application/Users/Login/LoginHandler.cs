using MediatR;
using Ixcent.CryptoTerminal.Domain.Database;
using Ixcent.CryptoTerminal.StorageHandle;
using Ixcent.CryptoTerminal.Application.Interfaces;
using Ixcent.CryptoTerminal.Application.Users.Services;

using Microsoft.AspNetCore.Identity;

namespace Ixcent.CryptoTerminal.Application.Users.Login
{
    public class LoginHandler : IRequestHandler<LoginQuery, User>
    {
        private readonly UsersService _service;

        // TODO : make UsersService as singleton
        public LoginHandler(CryptoTerminalContext context, UserManager<AppUser> userManager,
                            SignInManager<AppUser> signInManager,
                            IJwtGenerator jwtGenerator)
        {
            _service = new UsersService(context, userManager, signInManager, jwtGenerator);
        }

        public async Task<User> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            return await _service.Login(request.Email, request.Password);
        }
    }
}
