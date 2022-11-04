namespace Ixcent.CryptoTerminal.Domain.Users.Models.Repository
{
    public class User
    {
        public string Username { get; set; }
        
        public string Email { get; set; }
        
        public bool EmailConfirmed { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public bool PhoneNumberConfirmed { get; set; }
    }
}