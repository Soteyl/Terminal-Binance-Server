using Ixcent.CryptoTerminal.Domain.Database;
using Microsoft.AspNetCore.Identity;

namespace Ixcent.CryptoTerminal.Application.Users
{
    
    public interface IUsersRepository
    {
        public Task<IdentityResult> Create(AppUser user, string password);
        public Task<IQueryable<AppUser>> Read();
        public Task<IQueryable<AppUser>> Read(Func<AppUser, bool> expression);
        public Task Delete(AppUser user);
        public Task Update(AppUser user);
    }
}