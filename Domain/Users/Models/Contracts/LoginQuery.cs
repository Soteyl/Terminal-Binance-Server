using Ixcent.CryptoTerminal.Domain.Common.Interfaces;
using Ixcent.CryptoTerminal.Domain.Users.Models.Service;

namespace Ixcent.CryptoTerminal.Domain.Users.Models.Contracts
{
    public class LoginQuery : IRequestBase<User>
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
