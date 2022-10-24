using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ixcent.CryptoTerminal.Domain.Database;
using Ixcent.CryptoTerminal.StorageHandle;
using Ixcent.CryptoTerminal.Application.Status;
using Ixcent.CryptoTerminal.Application.Exceptions;
using Ixcent.CryptoTerminal.Application.Interfaces;

namespace Ixcent.CryptoTerminal.Application.Users.Services
{
    public class UsersService : IUsersService
    {
        private IUsersRepository _repository;
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private IJwtGenerator _jwtGenerator;
        
        public UsersService(CryptoTerminalContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtGenerator jwtGenerator)
        {
            // TODO : create a separate singleton ASP.NET service with injection
            _repository = new UsersManagerRepository(context, _userManager);
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
        }

        // TODO : replace with a class to avoid multiple parameters
        public async Task<User> Register(string email, string username, string password)
        {
            if (await _repository.Read(x => x.Email == email).Result.AnyAsync(CancellationToken.None))
            {
                throw new ServerException(ServerResponseCode.EmailAlreadyExists, "Email already exists");
            }

            if (await _repository.Read(x => x.Email == email).Result.AnyAsync(CancellationToken.None))
            {
                throw new ServerException(ServerResponseCode.UserAlreadyExists, "Username already exists" );
            }

            AppUser user = new()
            {
                Email = email,
                UserName = username
            };
            
            IdentityResult result = await _repository.Create(user, password);
            
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

            throw new ServerException(ServerResponseCode.UnknownError,
                "Client creation failed");
        }

        // TODO : replace with a class to avoid multiple parameters
        public async Task<User> Login(string email, string password)
        {
            AppUser? user = _repository.Read(user => user.Email == email).Result.First();

            if (user == null)
            {
                throw new ServerException(ServerResponseCode.UserFailedToAuthorize, "Invalid login/password");
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (result.Succeeded)
            {
                return new User
                {
                    Email = user.Email,
                    Token = _jwtGenerator.CreateToken(user),
                    UserName = user.UserName
                };
            }

            throw new ServerException(ServerResponseCode.UserFailedToAuthorize,
                "Client authorization failed");
        }
    }
}