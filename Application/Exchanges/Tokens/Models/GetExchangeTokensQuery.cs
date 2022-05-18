using MediatR;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Models
{
    /// <summary>
    /// Empty query object for getting available exchange tokens for user. <para/>
    /// Implements <see cref="IRequest{TResponse}"/> <br/>
    /// <c>IResponse</c> is <see cref="ExchangeTokensResult"/>
    /// </summary>
    public class GetExchangeTokensQuery : IRequest<ExchangeTokensResult>
    { }
}
