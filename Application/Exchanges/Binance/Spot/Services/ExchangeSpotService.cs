using Ixcent.CryptoTerminal.Domain.Common.Models;
using Ixcent.CryptoTerminal.Domain.Exchanges.Common.Interfaces;
using Ixcent.CryptoTerminal.Domain.Exchanges.Common.Models;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Interfaces;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Service;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Services
{
    public class ExchangeSpotService
    {
        private readonly IExchangeTokenService _tokenService;
        private readonly ISpotBalanceRepository _spotRepository;

        public ExchangeSpotService(IExchangeTokenService tokenService, ISpotBalanceRepository spotRepository)
        {
            _tokenService = tokenService;
            _spotRepository = spotRepository;
        }

        public async Task<Response<IEnumerable<CommonOrder>>> GetOpenOrders(string userId,
            CancellationToken cancellationToken = default)
        {
            Response<IEnumerable<CheckedExchangeToken>> tokens =
                await _tokenService.GetTokensByUserId(userId, cancellationToken);
            
            if (!tokens.IsSuccess || tokens.Result.)

            throw new NotImplementedException();
        }
    }
}