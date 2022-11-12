using FluentValidation;

using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Repository;

namespace Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Service
{
    public class AddTokenRequest
    {
        public string UserId { get; set; } = string.Empty;
        
        public string Exchange { get; set; }
        
        public string Key { get; set; }
        
        public string Secret { get; set; }
    }

    public class AddTokenRequestValidator : AbstractValidator<AddTokenRequest>
    {
        public AddTokenRequestValidator()
        {
            RuleFor(m => m.Exchange)
                .NotEmpty();
            RuleFor(m => m.Key)
                .NotEmpty();
            RuleFor(m => m.Secret)
                .NotEmpty();
            RuleFor(m => m.UserId)
                .NotEmpty();
        }
        
    }
}