using Microsoft.AspNetCore.Authorization;

namespace Ixcent.CryptoTerminal.Domain.Users.Models.Contracts
{
    public class IpCheckRequirement : IAuthorizationRequirement
    {
        public bool IpClaimRequired { get; set; } = true;
    }
}
