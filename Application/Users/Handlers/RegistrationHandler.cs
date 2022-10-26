using AutoMapper;

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
        private readonly IMapper _mapper;

        public RegistrationHandler(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Response<User>> Handle(RegistrationQuery request, CancellationToken cancellationToken)
        {
            return await _service.Register(_mapper.Map<RegisterData>(request));
        }
    }
}