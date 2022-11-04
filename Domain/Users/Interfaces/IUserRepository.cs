using Ixcent.CryptoTerminal.Domain.Database;
using Ixcent.CryptoTerminal.Domain.Users.Models.Repository;

using Microsoft.AspNetCore.Identity;

namespace Ixcent.CryptoTerminal.Domain.Users.Interfaces
{
    public interface IUserRepository
    {
        public Task<RegisterResult> Register(RegisterRequest request, CancellationToken cancellationToken = default);

        Task<LoginResult> Login(LoginRequest request, CancellationToken cancellationToken = default);
        
        public Task<GetByEmailResult> GetOneByEmail(string email, CancellationToken cancellationToken = default);

        Task<GetByUsernameResult> GetOneByUserName(string userName, CancellationToken cancellationToken = default);
    }
}