using AutoMapper;

using Ixcent.CryptoTerminal.Application.Users.Services;
using Ixcent.CryptoTerminal.Domain.Common.Interfaces;
using Ixcent.CryptoTerminal.Domain.Common.Models;
using Ixcent.CryptoTerminal.Domain.Users.Interfaces;
using Ixcent.CryptoTerminal.Domain.Users.Models.Handler;
using Ixcent.CryptoTerminal.Domain.Users.Models.Service;

namespace Ixcent.CryptoTerminal.Application.Users.Handlers
{
    public class LoginHandler : IRequestHandlerBase<LoginQuery, User>
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public LoginHandler(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Response<User>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            return await _service.Login(_mapper.Map<LoginData>(request));
        }
    }
}
