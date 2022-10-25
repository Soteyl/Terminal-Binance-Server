using Microsoft.AspNetCore.Identity;
using Ixcent.CryptoTerminal.Domain.Database;
using Ixcent.CryptoTerminal.StorageHandle;
using Ixcent.CryptoTerminal.Application.Exceptions;
using Ixcent.CryptoTerminal.Domain.Common;
using Ixcent.CryptoTerminal.Domain.Common.Interfaces;
using Ixcent.CryptoTerminal.Domain.Common.Models;
using Ixcent.CryptoTerminal.Domain.Users.Interfaces;
using Ixcent.CryptoTerminal.Domain.Users.Models.Service;

namespace Ixcent.CryptoTerminal.Application.Users.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtGenerator _jwtGenerator;
        
        public UserService(CryptoTerminalContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtGenerator jwtGenerator, IUserRepository repository)
        {
            _repository = repository;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<Response<User>> Register(RegisterData data)
        {
            if (await _repository.GetOneByMail(data.Email) != null)
            {
                return Response.WithError<User>(ServerResponseCode.InvalidApiToken);
            }

            if (await _repository.GetOneByMail(data.Email) != null)
            {
                return Response.WithError<User>(ServerResponseCode.UserAlreadyExists);
            }

            AppUser user = new()
            {
                Email = data.Email,
                UserName = data.UserName
            };
            
            IdentityResult result = await _repository.Create(user, data.Password);
            
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "user");
                return Response.Success(new User
                {
                    Token = _jwtGenerator.CreateToken(user),
                    UserName = user.UserName,
                    Email = user.Email
                });
            }
            return Response.WithError<User>(ServerResponseCode.UnknownError);
        }

        public async Task<Response<User>> Login(LoginData data)
        {
            AppUser? user = await _repository.GetOneByMail(data.Email);

            if (user == null)
            {
                throw new ServerException(ServerResponseCode.UserFailedToAuthorize, "Invalid login/password");
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, data.Password, false);

            if (result.Succeeded)
            {
                return Response.Success(new User
                {
                    Email = user.Email,
                    Token = _jwtGenerator.CreateToken(user),
                    UserName = user.UserName
                });
            }

            return Response.WithError<User>(ServerResponseCode.UserFailedToAuthorize);
        }
    }
}