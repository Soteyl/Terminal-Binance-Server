using FluentValidation;

namespace Ixcent.CryptoTerminal.Application.Validation
{
    using Application.Users.Registration;
    using Application.Validators;

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