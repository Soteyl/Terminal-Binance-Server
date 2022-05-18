using FluentValidation;
using Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixcent.CryptoTerminal.Application.Validation
{
    public class UpdateExchangeTokenCommandValidator : AbstractValidator<UpdateExchangeTokenQuery>
    {
        public UpdateExchangeTokenCommandValidator()
        {
            RuleFor(x => x.Key).NotEmpty();

            RuleFor(x => x.Secret).NotEmpty();

            RuleFor(x => x.Exchange).NotEmpty();
        }
    }
}
