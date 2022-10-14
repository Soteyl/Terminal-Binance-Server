using Ixcent.CryptoTerminal.Application.Status;

using MediatR;

namespace Ixcent.CryptoTerminal.Application.Mediatr
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