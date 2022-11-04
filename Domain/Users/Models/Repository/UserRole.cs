namespace Ixcent.CryptoTerminal.Domain.Users.Models.Repository
{
    public class UserRole
    {
        private UserRole(string name)
        {
            Name = name;
        }
        
        public string Name { get; }

        public static UserRole User { get; } = new UserRole("user");
    }
}