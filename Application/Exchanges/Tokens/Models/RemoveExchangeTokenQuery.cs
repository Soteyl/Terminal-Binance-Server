using MediatR;


namespace Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Models
{
    public class RemoveExchangeTokenQuery : IRequest
    {
        public string Exchange { get; set; } = string.Empty;
    }
}
