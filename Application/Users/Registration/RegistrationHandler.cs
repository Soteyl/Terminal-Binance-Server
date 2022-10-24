using MediatR;
using Microsoft.AspNetCore.Identity;
using Ixcent.CryptoTerminal.StorageHandle;
using Ixcent.CryptoTerminal.Domain.Database;
using Ixcent.CryptoTerminal.Application.Interfaces;
using Ixcent.CryptoTerminal.Application.Users.Services;

namespace Ixcent.CryptoTerminal.Application.Users.Registration
{
    public class RegistrationHandler : IRequestHandler<RegistrationCommand, User>
    {
        private readonly UsersService _service;

        // TODO : make UsersService as singleton
        public RegistrationHandler(CryptoTerminalContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtGenerator jwtGenerator)
        {
            _service = new UsersService(context, userManager, signInManager, jwtGenerator);
        }

        public async Task<User> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            return await _service.Register(
                email: request.Email, 
                username: request.UserName, 
                password: request.Password
                );
        }
    }
}