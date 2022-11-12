using Ixcent.CryptoTerminal.Domain.Common.Models;
using Ixcent.CryptoTerminal.Domain.Exchanges.Common.Models;
using CommonGetOpenOrdersRequest = Ixcent.CryptoTerminal.Domain.Exchanges.Common.Repository.CommonGetOpenOrdersRequest;

namespace Ixcent.CryptoTerminal.Domain.Exchanges.Common.Interfaces
{
    public interface ISpotBalanceRepository
    {
        Task<Response<IEnumerable<CommonOrder>>> GetTokensByUserId(CommonGetOpenOrdersRequest request, CancellationToken cancellationToken = default);
    }
}