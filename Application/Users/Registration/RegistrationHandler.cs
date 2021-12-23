using System.Net;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ixcent.CryptoTerminal.Application.Users.Registration
{
    using Application.Exceptions;
    using Application.Interfaces;
    using Domain.Database;
    using EFData;

    public class RegistrationHandler : IRequestHandler<RegistrationCommand, User>
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly IJwtGenerator _jwtGenerator;

        private readonly CryptoTerminalContext _context;

        public RegistrationHandler(CryptoTerminalContext context, UserManager<AppUser> userManager, IJwtGenerator jwtGenerator)
        {
            _context = context;
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<User> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Users.Where(x => x.Email == request.Email).AnyAsync())
            {
                throw new RestException(HttpStatusCode.BadRequest, new { Email = "Email already exist" });
            }

            if (await _context.Users.Where(x => x.UserName == request.UserName).AnyAsync())
            {
                throw new RestException(HttpStatusCode.BadRequest, new { UserName = "UserName already exist" });
            }

            var user = new AppUser
            {
                Email = request.Email,
                UserName = request.UserName
            };

            IdentityResult? result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                return new User
                {
                    Token = _jwtGenerator.CreateToken(user),
                    UserName = user.UserName,
                    Email = user.Email
                };
            }

            throw new Exception("Client creation failed");
        }
    }
}