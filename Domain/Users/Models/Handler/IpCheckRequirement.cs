using Microsoft.AspNetCore.Authorization;

namespace Ixcent.CryptoTerminal.Domain.Users.Models.Handler
{
    public class IpCheckRequirement : IAuthorizationRequirement
    {
        public bool IpClaimRequired { get; set; } = true;
    }
}
