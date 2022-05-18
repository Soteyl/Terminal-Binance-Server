using MediatR;


namespace Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Models
{
    public class RemoveExchangeTokenCommand : IRequest<ExchangeTokenResult>
    {
        public string Exchange { get; set; } = string.Empty;
    }
}
