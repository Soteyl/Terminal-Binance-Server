using Ixcent.CryptoTerminal.Application.Users.Services;
using Ixcent.CryptoTerminal.Domain.Common.Interfaces;
using Ixcent.CryptoTerminal.Domain.Common.Models;
using Ixcent.CryptoTerminal.Domain.Users.Interfaces;
using Ixcent.CryptoTerminal.Domain.Users.Models.Contracts;
using Ixcent.CryptoTerminal.Domain.Users.Models.Service;

namespace Ixcent.CryptoTerminal.Application.Users.Handlers
{
    public class RegistrationHandler : IRequestHandlerBase<RegistrationQuery, User>
    {
        private readonly IUserService _service;

        public RegistrationHandler(IUserService service)
        {
            _service = service;
        }

        public async Task<Response<User>> Handle(RegistrationQuery request, CancellationToken cancellationToken)
        {
            return await _service.Register(new RegisterData {
                Email = request.Email, 
                UserName = request.UserName, 
                Password = request.Password
            });
        }
    }
}