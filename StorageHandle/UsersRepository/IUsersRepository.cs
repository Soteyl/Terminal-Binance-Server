using Ixcent.CryptoTerminal.Domain.Database;

namespace Ixcent.CryptoTerminal.Application.Users
{
    
    public interface IUsersRepository
    {
        public Task Create(params AppUser[] users);
        public Task<IEnumerable<AppUser>> Read();
        public Task<IEnumerable<AppUser>> Read(Func<AppUser, bool> expression);
        public Task Delete(params AppUser[] users);
        public Task Update(params AppUser[] users);
    }
}