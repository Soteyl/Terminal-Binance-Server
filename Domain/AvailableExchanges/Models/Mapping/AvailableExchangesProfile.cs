using AutoMapper;

using Ixcent.CryptoTerminal.Domain.AvailableExchanges.Models.Data;

using Repository = Ixcent.CryptoTerminal.Domain.AvailableExchanges.Models.Repository;
using Service = Ixcent.CryptoTerminal.Domain.AvailableExchanges.Models.Service;

namespace Ixcent.CryptoTerminal.Domain.AvailableExchanges.Models.Mapping
{
    public class AvailableExchangesProfile : Profile
    {

        public AvailableExchangesProfile()
        {
            CreateMap<Service.GetAvailableExchangeRequest, Repository.GetAvailableExchangeRequest>();
            CreateMap<AvailableExchangeEntity, Service.AvailableExchange>()
                .ForMember(s => s.Id, o => o.MapFrom(d => d.Id))
                .ForMember(s => s.Name, o => o.MapFrom(d => d.Name));
        }
        
    }
}