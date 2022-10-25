using FluentValidation;

using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Contracts;

namespace Ixcent.CryptoTerminal.Application.Validation
{
    public class RemoveExchangeTokenCommandValidator : AbstractValidator<RemoveExchangeTokenQuery>
    {
        public RemoveExchangeTokenCommandValidator()
        {
            RuleFor(x => x.Exchange).NotEmpty();
        }
    }
}
