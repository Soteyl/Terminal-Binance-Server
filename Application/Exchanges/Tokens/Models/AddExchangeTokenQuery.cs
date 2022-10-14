using Ixcent.CryptoTerminal.Application.Mediatr;

using MediatR;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Models
{
    /// <summary>
    /// Query for adding exchange token to a database<para/>
    /// Implements <see cref="IRequest"/>
    /// </summary>
    public class AddExchangeTokenQuery : IRequestBase
    {
        /// <summary>
        /// Crypto exchange token key
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Crypto exchange token secret
        /// </summary>
        public string Secret { get; set; } = string.Empty;

        /// <summary>
        /// Crypto exchange name
        /// </summary>
        public string Exchange { get; set; } = string.Empty;
    }
}
