using Ixcent.CryptoTerminal.Application.Users;
using Ixcent.CryptoTerminal.Domain.Database;
using Ixcent.CryptoTerminal.Domain.Users.Interfaces;

using Microsoft.AspNetCore.Identity;

namespace Ixcent.CryptoTerminal.StorageHandle.UserRepository
{
    public class UserManagerRepository : IUserRepository
    {
        private UserManager<AppUser> _userManager;

        private CryptoTerminalContext _context;

        public UserManagerRepository(CryptoTerminalContext context, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public Task<IdentityResult> Create(AppUser user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }

        public Task<AppUser?> GetOneByMail(string mail)
        {
            return Task.FromResult(_context.Users.FirstOrDefault(u => u.Email == mail));
        }

        public Task<IQueryable<AppUser>> Read()
        {
            return Task.FromResult(_context.Users.ToList().AsQueryable());
        }

        public Task<IQueryable<AppUser>> Read(Func<AppUser, bool> expression)
        {
            return Task.FromResult(_context.Users.Where(expression).AsQueryable());
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