using FluentValidation;

namespace Ixcent.CryptoTerminal.Application.Validation
{
    using Users.Login;

    public class LoginQueryValidation : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidation()
        {
            RuleFor(x => x.Email).NotEmpty();

            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
