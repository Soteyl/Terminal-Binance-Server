using AutoMapper;

using Ixcent.CryptoTerminal.Domain.AvailableExchanges.Interfaces;
using Ixcent.CryptoTerminal.Domain.AvailableExchanges.Models.Repository;
using Ixcent.CryptoTerminal.Domain.AvailableExchanges.Models.Service;
using Ixcent.CryptoTerminal.Storage;
using GetAvailableExchangeRequest = Ixcent.CryptoTerminal.Domain.AvailableExchanges.Models.Repository.GetAvailableExchangeRequest;

namespace Ixcent.CryptoTerminal.Infrastructure.AvailableExchanges
{
    public class AvailableExchangesRepository : IAvailableExchangeRepository
    {
        private readonly CryptoTerminalContext _context;
        private readonly IMapper _mapper;

        public AvailableExchangesRepository(CryptoTerminalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public Task<GetAvailableExchangesResult> Get(GetAvailableExchangeRequest request, CancellationToken cancellationToken)
        {
            var result = new GetAvailableExchangesResult
            {
                AvailableExchanges = _context.AvailableExchanges.Select(x => _mapper.Map<AvailableExchange>(x)).ToList()
            };
            return Task.FromResult(result);
        }
        
    }
}