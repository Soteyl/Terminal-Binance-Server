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
            Repository.GetAvailableExchangeRequest request = _mapper.Map<Repository.GetAvailableExchangeRequest>(getAvailableExchangeRequest);
            Repository.GetAvailableExchangesResult repositoryResult = await _repository.Get(request, cancellationToken);
            GetAvailableExchangeResponse response = _mapper.Map<GetAvailableExchangeResponse>(repositoryResult);
            return Response.Success(response);
        }
    }
}