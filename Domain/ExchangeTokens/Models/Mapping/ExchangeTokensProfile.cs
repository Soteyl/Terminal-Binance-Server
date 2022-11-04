using AutoMapper;

using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Data;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Handler;

namespace Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Mapping
{
    public class ExchangeTokensProfile: Profile
    {
        public ExchangeTokensProfile()
        {
            CreateMap<AddExchangeTokenQuery, Service.AddTokenRequest>();
            CreateMap<Repository.UserExchange, Service.RemoveTokenRequest>().ReverseMap();

            CreateMap<Controller.AddExchangeTokenQuery, Handler.AddExchangeTokenQuery>();

            CreateMap<Repository.AddTokenRequest, ExchangeTokenEntity>()
                .ForMember(s => s.Key, o => o.MapFrom(d => d.Token.Key))
                .ForMember(s => s.Secret, o => o.MapFrom(d => d.Token.Secret));
        }
    }
}