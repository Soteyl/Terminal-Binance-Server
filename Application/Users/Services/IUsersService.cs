namespace Ixcent.CryptoTerminal.Application.Users.Services
{
    public interface IUsersService
    {
        Task<User> Register(string email, string username, string password);
        Task<User> Login(string email, string password);
    }
}