using Repository = Ixcent.CryptoTerminal.Domain.AvailableExchanges.Models.Repository;

namespace Ixcent.CryptoTerminal.Domain.AvailableExchanges.Interfaces
{
    public interface IAvailableExchangeRepository
    {
        Task<Repository.GetAvailableExchangesResult> Get(Repository.GetAvailableExchangeRequest request, CancellationToken cancellationToken);
    }

}