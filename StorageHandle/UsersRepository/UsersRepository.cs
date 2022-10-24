using Ixcent.CryptoTerminal.Domain.Database;
using Ixcent.CryptoTerminal.StorageHandle;

namespace Ixcent.CryptoTerminal.Application.Users
{
    public class UsersRepository : IUsersRepository
    {
        public CryptoTerminalContext _context;

        public async Task Create(params AppUser[] users)
        {
            await _context.Users.AddRangeAsync(users);
            await _context.SaveChangesAsync();
        }
        
        public Task<IEnumerable<AppUser>> Read()
        {
            return Task.FromResult(_context.Users.ToList().AsEnumerable());
        }
        
        public Task<IEnumerable<AppUser>> Read(Func<AppUser, bool> expression)
        {
            return Task.FromResult(_context.Users.ToList().AsEnumerable());
        }

        public async Task Delete(params AppUser[] users)
        {
            _context.Users.RemoveRange(users);
            await _context.SaveChangesAsync();
        }

        public async Task Update(params AppUser[] users)
        {
            _context.Users.UpdateRange(users);
            await _context.SaveChangesAsync();
        }
        
    }

}