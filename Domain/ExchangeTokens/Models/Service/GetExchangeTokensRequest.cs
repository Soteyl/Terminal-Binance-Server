using FluentValidation;

namespace Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Service
{
    public class GetExchangeTokensRequest {
        public string UserId { get; set; }
    }

    public class GetExchangeTokenRequestValidator : AbstractValidator<GetExchangeTokensRequest>
    {
        public GetExchangeTokenRequestValidator()
        {
            RuleFor(m => m.UserId).NotEmpty();
        }
    }
}
