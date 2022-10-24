using Ixcent.CryptoTerminal.Domain.Database;
using Ixcent.CryptoTerminal.StorageHandle;

using Microsoft.AspNetCore.Identity;

namespace Ixcent.CryptoTerminal.Application.Users
{
    public class UsersManagerRepository : IUsersRepository
    {
        private UserManager<AppUser> _userManager;

        private CryptoTerminalContext _context;

        public UsersManagerRepository(CryptoTerminalContext context, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public Task<IdentityResult> Create(AppUser user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }

        public Task<IQueryable<AppUser>> Read()
        {
            return Task.FromResult(_context.Users.ToList().AsQueryable());
        }

        public Task<IQueryable<AppUser>> Read(Func<AppUser, bool> expression)
        {
            return Task.FromResult(_context.Users.ToList().AsQueryable());
        }

        public async Task Delete(AppUser user)
        { 
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task Update(AppUser user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}