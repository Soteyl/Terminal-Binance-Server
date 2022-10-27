using FluentValidation;

namespace Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Repository
{
    public class Token
    {
        public string Key { get; set; }
        
        public string Secret { get; set; }

        public class TokenValidator : AbstractValidator<Token>
        {
            public TokenValidator()
            {
                RuleFor(s => s.Key)
                    .NotEmpty();
                
                RuleFor(s => s.Secret)
                    .NotEmpty();
            }
        }
    }
}