using Ixcent.CryptoTerminal.Domain.Database;

using Microsoft.AspNetCore.Identity;

namespace Ixcent.CryptoTerminal.Domain.Users.Interfaces
{
    
    public interface IUserRepository
    {
        public Task<IdentityResult> Create(AppUser user, string password);
        public Task<AppUser?> GetOneByMail(string mail);
        public Task Delete(AppUser user);
        public Task Update(AppUser user);
    }
}