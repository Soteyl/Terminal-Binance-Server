using FluentValidation;

using Ixcent.CryptoTerminal.Application.Users.Registration;
using Ixcent.CryptoTerminal.Application.Validators;

namespace Ixcent.CryptoTerminal.Application.Validation
{
    public class RegistrationValidation : AbstractValidator<RegistrationCommand>
    {
        public RegistrationValidation()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().Password();
        }
    }
}