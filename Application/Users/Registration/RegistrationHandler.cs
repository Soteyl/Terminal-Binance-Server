using Ixcent.CryptoTerminal.Application.Mediatr;
using Ixcent.CryptoTerminal.Application.Users.Services;

namespace Ixcent.CryptoTerminal.Application.Users.Registration
{
    public class RegistrationHandler : IRequestHandlerBase<RegistrationCommand, User>
    {
        private readonly IUserService _service;

        public RegistrationHandler(IUserService service)
        {
            _service = service;
        }

        public async Task<Response<User>> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            return await _service.Register(new RegisterData {
                Email = request.Email, 
                UserName = request.UserName, 
                Password = request.Password
            });
        }
    }
}