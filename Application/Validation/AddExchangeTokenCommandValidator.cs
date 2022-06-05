using FluentValidation;

using Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Models;

namespace Ixcent.CryptoTerminal.Application.Validation
{
    public class AddExchangeTokenCommandValidator : AbstractValidator<AddExchangeTokenQuery>
    {
        public AddExchangeTokenCommandValidator()
        {
            RuleFor(x => x.Key).NotEmpty();

            RuleFor(x => x.Secret).NotEmpty();

            RuleFor(x => x.Exchange).NotEmpty();
        }
    }
}
