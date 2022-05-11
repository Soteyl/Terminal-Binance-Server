using Microsoft.AspNetCore.Authorization;

namespace Ixcent.CryptoTerminal.Application.Users.IP
{
    public class IpCheckRequirement : IAuthorizationRequirement
    {
        public bool IpClaimRequired { get; set; } = true;
    }
}
