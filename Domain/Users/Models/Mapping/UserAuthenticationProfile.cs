using AutoMapper;

using Ixcent.CryptoTerminal.Domain.Users.Models.Contracts;
using Ixcent.CryptoTerminal.Domain.Users.Models.Service;

namespace Ixcent.CryptoTerminal.Domain.Users.Models.Mapping
{
    public class UserAuthenticationProfile: Profile
    {
        public UserAuthenticationProfile()
        {
            CreateMap<RegistrationQuery, RegisterData>();
            CreateMap<LoginQuery, LoginData>();
        }
    }
}