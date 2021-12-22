namespace CryptoTerminal.Models.Database
{
    /// <summary>
    /// Database model representing user role.
    /// </summary>
    public class UserRole
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<User> Users { get; set; }

        public UserRole()
        {
            Users = new List<User>();
        }
    }
}