using AutoMapper;

using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Data;

namespace Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Mapping
{
    public class ExchangeTokensProfile: Profile
    {
        public ExchangeTokensProfile()
        {
            CreateMap<Contracts.AddExchangeTokenQuery, Service.UserExchangeToken>();
            CreateMap<Repository.UserExchange, Service.UserExchange>().ReverseMap();

            CreateMap<Repository.AddTokenRequest, ExchangeTokenEntity>()
                .ForMember(s => s.Key, o => o.MapFrom(d => d.Token.Key))
                .ForMember(s => s.Secret, o => o.MapFrom(d => d.Token.Secret));
        }
    }
}