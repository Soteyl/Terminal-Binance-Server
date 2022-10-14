using Ixcent.CryptoTerminal.Application.Mediatr;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Models
{
    /// <summary>
    /// Empty query object for getting available exchange tokens for user. <para/>
    /// </summary>
    public class GetExchangeTokensQuery : IRequestBase<ExchangeTokensResult>
    { }
}
