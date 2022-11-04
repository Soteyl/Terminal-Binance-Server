namespace Ixcent.CryptoTerminal.Domain.Users.Models.Repository
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        
        public string UserName { get; set; }
        
        public string Password { get; set; }
        
        public UserRole Role { get; set; }
    }
}