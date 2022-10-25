using Ixcent.CryptoTerminal.Application.Mediatr;

namespace Ixcent.CryptoTerminal.Application.Users.Services
{
    public interface IUserService
    {
        Task<Response<User>> Register(RegisterData data);
        Task<Response<User>> Login(LoginData data);
    }
}