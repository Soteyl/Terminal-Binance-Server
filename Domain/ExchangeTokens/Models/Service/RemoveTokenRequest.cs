using FluentValidation;

namespace Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Service
{
    public class RemoveTokenRequest
    {
        public string UserId { get; set; }
        
        public string Exchange { get; set; }
    }

    public class RemoveTokenRequestValidator : AbstractValidator<RemoveTokenRequest>
    {
        public RemoveTokenRequestValidator()
        {
            RuleFor(m => m.UserId).NotEmpty();
            RuleFor(m => m.Exchange).NotEmpty();
        }
    }
}