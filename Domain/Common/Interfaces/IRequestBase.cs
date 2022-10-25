using Ixcent.CryptoTerminal.Domain.Common.Models;

using MediatR;

namespace Ixcent.CryptoTerminal.Domain.Common.Interfaces
{
    public interface IRequestBase<TResponse> : IRequest<Response<TResponse>>
    {
    }

    public interface IRequestBase : IRequest<Response>
    {
    }
}