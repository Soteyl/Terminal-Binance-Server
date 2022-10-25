using Ixcent.CryptoTerminal.Domain.Common.Interfaces;
using Ixcent.CryptoTerminal.Domain.Users.Models.Service;

namespace Ixcent.CryptoTerminal.Domain.Users.Models.Contracts
{
    public class RegistrationQuery : IRequestBase<User>
    {
        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}