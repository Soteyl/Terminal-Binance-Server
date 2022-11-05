using Ixcent.CryptoTerminal.Domain.Common.Models;
using Ixcent.CryptoTerminal.Domain.AvailableExchanges.Models.Service;

namespace Ixcent.CryptoTerminal.Domain.AvailableExchanges.Interfaces
{
    public interface IAvailableExchangeService
    {
        Task<Response<GetAvailableExchangeResponse>> Get(GetAvailableExchangeRequest getAvailableExchangeRequest, CancellationToken cancellationToken = default);
    }
}