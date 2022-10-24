namespace Ixcent.CryptoTerminal.Application.Users
{
    public class User
    {
        public static User EmptyUser => new(); 
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
