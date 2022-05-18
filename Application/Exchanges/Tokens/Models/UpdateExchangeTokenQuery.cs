using MediatR;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Models
{
    public class UpdateExchangeTokenQuery : IRequest
    {

        public string Key { get; set; } = string.Empty;

        public string Secret { get; set; } = string.Empty;

        public string Exchange { get; set; } = string.Empty;

    }
}
