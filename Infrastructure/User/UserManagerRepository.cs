using AutoMapper;

using Ixcent.CryptoTerminal.Domain.Common.Interfaces;
using Ixcent.CryptoTerminal.Domain.Database;
using Ixcent.CryptoTerminal.Domain.Users.Interfaces;
using Ixcent.CryptoTerminal.Domain.Users.Models.Repository;
using Ixcent.CryptoTerminal.Storage;

using Microsoft.AspNetCore.Identity;

namespace Ixcent.CryptoTerminal.Infrastructure.User
{
    public class UserManagerRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;

        private readonly IJwtGenerator _jwtGenerator;

        private readonly IMapper _mapper;

        public UserManagerRepository(CryptoTerminalContext context, UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, IJwtGenerator jwtGenerator, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
            _mapper = mapper;
        }

        public async Task<RegisterResult> Register(RegisterRequest request,
            CancellationToken cancellationToken = default)
        {
            var user = new AppUser { UserName = request.UserName, Email = request.Email };

            IdentityResult? result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, request.Role.Name);
                return _mapper.Map<RegisterResult>(Login(_mapper.Map<LoginRequest>(request), cancellationToken));
            }

            return new RegisterResult();
        }

        public async Task<LoginResult> Login(LoginRequest request, CancellationToken cancellationToken = default)
        {
            AppUser? user = await _userManager.FindByEmailAsync(request.Email);

            SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            var result = new LoginResult();
            if (signInResult.Succeeded)
                result.Token = _jwtGenerator.CreateToken(user);

            return result;
        }

        public async Task<GetByEmailResult> GetOneByEmail(string email, CancellationToken cancellationToken = default)
        {
            return new GetByEmailResult
            {
                User = _mapper.Map<Domain.Users.Models.Repository.User>(await _userManager.FindByEmailAsync(email))
            };
        }

        public async Task<GetByUsernameResult> GetOneByUserName(string userName,
            CancellationToken cancellationToken = default)
        {
            return new GetByUsernameResult
            {
                User = _mapper.Map<Domain.Users.Models.Repository.User>(
                    await _userManager.FindByNameAsync(userName))
            };
        }
    }
}