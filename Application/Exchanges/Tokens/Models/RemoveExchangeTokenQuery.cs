using Ixcent.CryptoTerminal.Application.Mediatr;

using MediatR;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Models
{
    /// <summary>
    /// Query object for removing crypto exchange tokens from database. <para/>
    /// Implements <see cref="IRequest"/>
    /// </summary>
    public class RemoveExchangeTokenQuery : IRequestBase
    {
        /// <summary>
        /// Name of crypto exchange where to remove key
        /// </summary>
        public string Exchange { get; set; } = string.Empty;
    }
}