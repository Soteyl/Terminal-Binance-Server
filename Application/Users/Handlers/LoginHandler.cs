using Ixcent.CryptoTerminal.Application.Users.Services;
using Ixcent.CryptoTerminal.Domain.Common.Interfaces;
using Ixcent.CryptoTerminal.Domain.Common.Models;
using Ixcent.CryptoTerminal.Domain.Users.Interfaces;
using Ixcent.CryptoTerminal.Domain.Users.Models.Contracts;
using Ixcent.CryptoTerminal.Domain.Users.Models.Service;

namespace Ixcent.CryptoTerminal.Application.Users.Handlers
{
    public class LoginHandler : IRequestHandlerBase<LoginQuery, User>
    {
        private readonly IUserService _service;

        public LoginHandler(IUserService service)
        {
            _service = service;
        }

        public async Task<Response<User>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            return await _service.Login(new LoginData
            {
                Email = request.Email, 
                Password = request.Password
            });
        }
    }
}
