using AutoMapper;

using Ixcent.CryptoTerminal.Domain.AvailableExchanges.Interfaces;
using Ixcent.CryptoTerminal.Domain.AvailableExchanges.Models.Service;
using Ixcent.CryptoTerminal.Domain.Common.Models;
using Repository = Ixcent.CryptoTerminal.Domain.AvailableExchanges.Models.Repository;
namespace Ixcent.CryptoTerminal.Application.AvailableExchanges.Services
{
    public class AvailableExchangesService : IAvailableExchangeService
    {
        private readonly IMapper _mapper;
        private readonly IAvailableExchangeRepository _repository;

        public AvailableExchangesService(IMapper mapper, IAvailableExchangeRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        
        public async Task<Response<GetAvailableExchangeResponse>> Get(GetAvailableExchangeRequest getAvailableExchangeRequest, CancellationToken cancellationToken = default)
        {
            var request = _mapper.Map<Repository.GetAvailableExchangeRequest>(getAvailableExchangeRequest);
            var repositoryResult = await _repository.Get(request, cancellationToken);
            var response = _mapper.Map<GetAvailableExchangeResponse>(repositoryResult);
            return Response.Success(response);
        }
    }
}