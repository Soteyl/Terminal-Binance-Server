using FluentValidation;

using Ixcent.CryptoTerminal.Application.Users.Login;

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
