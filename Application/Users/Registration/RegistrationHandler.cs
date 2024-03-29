﻿using System.Net;

using Ixcent.CryptoTerminal.Application.Exceptions;
using Ixcent.CryptoTerminal.Application.Interfaces;
using Ixcent.CryptoTerminal.Domain.Database;
using Ixcent.CryptoTerminal.EFData;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ixcent.CryptoTerminal.Application.Users.Registration
{
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
            if (await _context.Users.Where(x => x.Email == request.Email).AnyAsync(CancellationToken.None))
            {
                throw new RestException(HttpStatusCode.BadRequest,
                                        ErrorCode.AlreadyExist,
                                        new { Email = "Email already exists" });
            }

            if (await _context.Users.Where(x => x.UserName == request.UserName).AnyAsync(CancellationToken.None))
            {
                throw new RestException(HttpStatusCode.BadRequest,
                                        ErrorCode.AlreadyExist,
                                        new { Username = "Username already exists" });
            }

            var user = new AppUser
            {
                Email = request.Email,
                UserName = request.UserName
            };

            IdentityResult? result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "user");
                return new User
                {
                    Token = _jwtGenerator.CreateToken(user),
                    UserName = user.UserName,
                    Email = user.Email
                };
            }

            throw new RestException(HttpStatusCode.InternalServerError,
                                    ErrorCode.Unknown,
                                    new { Message = "Client creation failed" });
        }
    }
}