using Ixcent.CryptoTerminal.Domain.Common.Models;
using Ixcent.CryptoTerminal.Domain.Users.Models.Service;

namespace Ixcent.CryptoTerminal.Domain.Users.Interfaces
{
    public interface IUserService
    {
        Task<Response<User>> Register(RegisterData data);
        Task<Response<User>> Login(LoginData data);
    }
}