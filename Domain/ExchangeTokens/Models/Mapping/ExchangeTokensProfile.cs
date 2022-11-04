using AutoMapper;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Data;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Handler;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Service;

using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Controller;

using AddExchangeTokenQuery = Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Handler.AddExchangeTokenQuery;

namespace Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Mapping
{
    public class ExchangeTokensProfile: Profile
    {
        public ExchangeTokensProfile()
        {
            CreateMap<AddExchangeTokenQuery, AddTokenRequest>();
            CreateMap<Controller.RemoveExchangeTokenQuery, RemoveTokenRequest>();
            CreateMap<GetExchangeTokensRequest, GetExchangeTokensRequest>();
           
            CreateMap<Repository.UserExchange, Service.RemoveTokenRequest>().ReverseMap();

            CreateMap<Controller.AddExchangeTokenQuery, Handler.AddExchangeTokenQuery>();
            CreateMap<Controller.RemoveExchangeTokenQuery, Handler.RemoveExchangeTokenQuery>();
            CreateMap<Controller.GetExchangeTokensQuery, Handler.GetExchangeTokensQuery>();
            
            CreateMap<Repository.AddTokenRequest, ExchangeTokenEntity>()
                .ForMember(s => s.Key, o => o.MapFrom(d => d.Token.Key))
                .ForMember(s => s.Secret, o => o.MapFrom(d => d.Token.Secret));
        }
    }
}