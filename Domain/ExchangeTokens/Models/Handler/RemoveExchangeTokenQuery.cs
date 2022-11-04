using FluentValidation;

using Ixcent.CryptoTerminal.Domain.Common.Interfaces;

using MediatR;

namespace Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Handler
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
        
        public string UserId { get; set; }
    }
    
    public class RemoveExchangeTokenQueryValidator : AbstractValidator<RemoveExchangeTokenQuery>
    {
        public RemoveExchangeTokenQueryValidator()
        {
            RuleFor(m => m.Exchange)
                .NotEmpty();

            RuleFor(m => m.UserId)
                .NotEmpty();
        }
    }
}