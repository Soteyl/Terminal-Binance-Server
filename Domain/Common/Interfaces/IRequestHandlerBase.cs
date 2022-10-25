using Ixcent.CryptoTerminal.Domain.Common.Models;

using MediatR;

namespace Ixcent.CryptoTerminal.Domain.Common.Interfaces
{
    public interface IRequestHandlerBase<in TRequest, TResponse> : IRequestHandler<TRequest, Response<TResponse>>
        where TRequest : IRequestBase<TResponse>
    {
    }

    public interface IRequestHandlerBase<in TRequest> : IRequestHandler<TRequest, Response>
        where TRequest : IRequestBase
    {
    }
}