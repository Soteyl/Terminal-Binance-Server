using AutoMapper;

using Binance.Net.Objects.Spot.SpotData;

using Ixcent.CryptoTerminal.Domain.Exchanges.Common.Models;

namespace Ixcent.CryptoTerminal.Domain.Exchanges.Binance.Spot.Models.Mapping
{
    public class BinanceSpotOrderProfile: Profile
    {
        public BinanceSpotOrderProfile()
        {
            CreateMap<BinanceOrder, CommonOrder>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.OrderId.ToString()))
                .ForMember(d => d.Price, o => o.MapFrom(s => s.Price))
                .ForMember(d => d.Quantity, o => o.MapFrom(s => s.Quantity))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString())) // TODO convertation status
                .ForMember(d => d.Symbol, o => o.MapFrom(s => s.Symbol));
        }
    }
}