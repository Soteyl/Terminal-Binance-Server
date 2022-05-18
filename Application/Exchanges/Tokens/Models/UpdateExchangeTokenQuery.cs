using MediatR;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Models
{
    /// <summary>
    /// Query object for editing information about crypto exchanges tokens <para/>
    /// Implements <see cref="IRequest"/>
    /// </summary>
    public class UpdateExchangeTokenQuery : IRequest
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
