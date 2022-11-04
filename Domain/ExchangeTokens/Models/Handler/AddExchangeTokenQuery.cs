using FluentValidation;

using Ixcent.CryptoTerminal.Domain.Common.Interfaces;

using MediatR;

namespace Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Handler
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
        
        public string UserId { get; set; }
    }

    public class AddExchangeTokenQueryValidator : AbstractValidator<AddExchangeTokenQuery>
    {
        public AddExchangeTokenQueryValidator()
        {
            RuleFor(m => m.Key)
                .NotEmpty();

            RuleFor(m => m.Secret)
                .NotEmpty();

            RuleFor(m => m.Exchange)
                .NotEmpty();

            RuleFor(m => m.UserId)
                .NotEmpty();
        }
    }
}
