namespace Ixcent.CryptoTerminal.Domain.Users.Models.Repository
{
    public class RegisterResult
    {
        public bool Success => !string.IsNullOrWhiteSpace(Token);
        
        public string Token { get; set; }
    }
}