using FluentValidation;

using Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Models;

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
