using Ixcent.CryptoTerminal.Domain.Database;
using Ixcent.CryptoTerminal.StorageHandle;

using Microsoft.AspNetCore.Identity;

namespace Ixcent.CryptoTerminal.Application.Users
{
    public class UsersRepository : IUsersRepository
    {
        public CryptoTerminalContext _context;
        
        public Task<IdentityResult> Create(AppUser user, string password)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<AppUser>> Read()
        {
            return Task.FromResult(_context.Users.ToList().AsQueryable());
        }
        
        public Task<IQueryable<AppUser>> Read(Func<AppUser, bool> expression)
        {
            return Task.FromResult(_context.Users.ToList().AsQueryable());
        }

        public Task Delete(AppUser user)
        {
            throw new NotImplementedException();
        }

        public Task Update(AppUser user)
        {
            throw new NotImplementedException();
        }

    }

}