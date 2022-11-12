using AutoMapper;

using Ixcent.CryptoTerminal.Domain.Exchanges.Common.Service;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Service;

namespace Ixcent.CryptoTerminal.Domain.Exchanges.Common.Mapping
{
    public class CommonExchangesProfile : Profile
    {
        public CommonExchangesProfile()
        {
            CreateMap<CommonGetOpenOrdersRequest, GetExchangeTokensRequest>();
        }
    }
}