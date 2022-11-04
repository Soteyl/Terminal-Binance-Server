using FluentValidation;

using Ixcent.CryptoTerminal.Application.Validators;
using Ixcent.CryptoTerminal.Domain.Users.Models.Handler;

namespace Ixcent.CryptoTerminal.Application.Validation
{
    public class RegistrationValidation : AbstractValidator<RegistrationQuery>
    {
        public RegistrationValidation()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().Password();
        }
    }
}