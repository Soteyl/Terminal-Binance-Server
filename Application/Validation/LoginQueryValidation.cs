using FluentValidation;

using Ixcent.CryptoTerminal.Domain.Users.Models.Handler;

namespace Ixcent.CryptoTerminal.Application.Validation
{
    public class LoginQueryValidation : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidation()
        {
            RuleFor(x => x.Email).NotEmpty();

            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
