using Ixcent.CryptoTerminal.Application.Mediatr;
using Ixcent.CryptoTerminal.Application.Users.Services;

namespace Ixcent.CryptoTerminal.Application.Users.Login
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
