using AutoMapper;

using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Contracts;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Service;

namespace Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Mapping
{
    public class ExchangeTokensProfile: Profile
    {
        public ExchangeTokensProfile()
        {
            CreateMap<AddExchangeTokenQuery, UserExchangeToken>();
        }
    }
}