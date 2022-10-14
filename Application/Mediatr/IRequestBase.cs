using MediatR;

namespace Ixcent.CryptoTerminal.Application.Mediatr
{
    public interface IRequestBase<TResponse> : IRequest<Response<TResponse>>
    {
    }

    public interface IRequestBase : IRequest<Response>
    {
    }
}