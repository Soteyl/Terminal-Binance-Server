using Ixcent.CryptoTerminal.Domain.Database;

namespace Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Data
{
    public class ExchangeTokenEntity
    {
        public long Id { get; set; }

        public string Secret { get; set; }

        public string Key { get; set; }

        public string Exchange { get; set; }

        public string UserId { get; set; }

        public AppUser User { get; set; }
    }
}
