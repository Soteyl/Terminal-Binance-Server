using AutoMapper;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Data;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Handler;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Service;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Controller;


namespace Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Mapping
{
    public class ExchangeTokensProfile: Profile
    {
        public ExchangeTokensProfile()
        {
            CreateMap<Controller.AddExchangeTokenQuery, Service.AddTokenRequest>();
            CreateMap<Controller.RemoveExchangeTokenQuery, Service.RemoveTokenRequest>();
            CreateMap<Controller.GetExchangeTokensQuery, Service.GetExchangeTokensRequest>();
           
            CreateMap<Repository.UserExchange, Service.RemoveTokenRequest>().ReverseMap();

            CreateMap<Service.AddTokenRequest, Handler.AddExchangeTokenQuery>();
            CreateMap<Service.RemoveTokenRequest, Handler.RemoveExchangeTokenQuery>();
            CreateMap<Service.GetExchangeTokensRequest, Handler.GetExchangeTokensQuery>();
            
            
            CreateMap<Repository.AddTokenRequest, ExchangeTokenEntity>()
                .ForMember(s => s.Key, o => o.MapFrom(d => d.Token.Key))
                .ForMember(s => s.Secret, o => o.MapFrom(d => d.Token.Secret));
        }
    }
}