using AutoMapper;

using Binance.Net;
using Binance.Net.Objects.Spot.SpotData;

using CryptoExchange.Net.Objects;

using Ixcent.CryptoTerminal.Application;
using Ixcent.CryptoTerminal.Domain.Common.Models;
using Ixcent.CryptoTerminal.Domain.Exchanges.Common.Interfaces;
using Ixcent.CryptoTerminal.Domain.Exchanges.Common.Models;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Repository;

namespace Ixcent.CryptoTerminal.Infrastructure.BinanceSpotBalance
{
    public class BinanceSpotBalanceRepository : ISpotBalanceRepository
    {
        private readonly IMapper _mapper;
        private readonly BinanceClient _client;

        public BinanceSpotBalanceRepository(Token token, IMapper mapper)
        {
            _mapper = mapper;
            _client = new BinanceClient();
            _client.SetApiCredentials(token.Key, token.Secret);
        }

        public async Task<Response<IEnumerable<CommonOrder>>> GetOpenOrders(
            CancellationToken cancellationToken = default)
        {
            WebCallResult<IEnumerable<BinanceOrder>> result =
                await _client.Spot.Order.GetOpenOrdersAsync(ct: cancellationToken);

            return result.Success
                ? Response.Success(result.Data.Select(o => _mapper.Map<CommonOrder>(o)))
                : result.ToErrorResponse<IEnumerable<CommonOrder>>();
        }
    }
}