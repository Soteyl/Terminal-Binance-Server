using Ixcent.CryptoTerminal.Domain.Database;
using Ixcent.CryptoTerminal.Domain.Users.Interfaces;
using Ixcent.CryptoTerminal.Storage;

using Microsoft.AspNetCore.Identity;

namespace Ixcent.CryptoTerminal.Infrastructure.User
{
    public class UserManagerRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly CryptoTerminalContext _context;

        public UserManagerRepository(CryptoTerminalContext context, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public Task<IdentityResult> Create(AppUser user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }

        public Task<AppUser> GetOneByMail(string mail)
        {
            return _userManager.FindByEmailAsync(mail);
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