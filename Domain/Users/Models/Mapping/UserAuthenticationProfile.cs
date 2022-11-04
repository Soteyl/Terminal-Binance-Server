using AutoMapper;

using Ixcent.CryptoTerminal.Domain.Database;
using Ixcent.CryptoTerminal.Domain.Users.Models.Handler;
using Ixcent.CryptoTerminal.Domain.Users.Models.Repository;
using Ixcent.CryptoTerminal.Domain.Users.Models.Service;

using User = Ixcent.CryptoTerminal.Domain.Users.Models.Repository.User;

namespace Ixcent.CryptoTerminal.Domain.Users.Models.Mapping
{
    public class UserAuthenticationProfile: Profile
    {
        public UserAuthenticationProfile()
        {
            CreateMap<RegistrationQuery, RegisterData>();
            CreateMap<LoginQuery, LoginData>();
            CreateMap<RegisterRequest, LoginRequest>();
            CreateMap<LoginResult, RegisterResult>();
            CreateMap<AppUser, User>();
        }
    }
}