using AutoMapper;

using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Interfaces;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Service;

using Ixcent.CryptoTerminal.Domain.Common.Models;
using Ixcent.CryptoTerminal.Domain.Exchanges.Common.Interfaces;
using Ixcent.CryptoTerminal.Domain.Exchanges.Common.Models;
using Ixcent.CryptoTerminal.Domain.Exchanges.Common.Service;

using Repository = Ixcent.CryptoTerminal.Domain.Exchanges.Common.Repository;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Services
{
    public class ExchangeSpotService
    {
        private readonly IExchangeTokenService _tokenService;
        private readonly ISpotBalanceRepository _spotRepository;
        private readonly IMapper _mapper;

        public ExchangeSpotService(IExchangeTokenService tokenService, ISpotBalanceRepository spotRepository, IMapper mapper)
        {
            _tokenService = tokenService;
            _spotRepository = spotRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<CommonOrder>>> GetOpenOrders(CommonGetOpenOrdersRequest getOrdersRequest, CancellationToken cancellationToken = default)
        {
            GetExchangeTokensRequest request = _mapper.Map<GetExchangeTokensRequest>(getOrdersRequest);
            Response<GetTokensResponse> tokens = await _tokenService.Get(request, cancellationToken);

            // if (_spotRepository)
            
            throw new NotImplementedException();
        }
    }
}