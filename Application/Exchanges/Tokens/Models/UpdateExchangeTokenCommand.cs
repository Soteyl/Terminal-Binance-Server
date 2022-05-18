using MediatR;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Models
{
    public class UpdateExchangeTokenCommand : IRequest<ExchangeTokenResult>
    {

        public string Token { get; set; } = string.Empty;

        public string Secret { get; set; } = string.Empty;

        public string Exchange { get; set; } = string.Empty;

    }
}
