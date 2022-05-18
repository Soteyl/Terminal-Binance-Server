namespace Ixcent.CryptoTerminal.Domain.Database.Models
{
    public class ExchangeToken
    {
        public long Id { get; set; }

        public string Secret { get; set; }

        public string Key { get; set; }

        public string Exchange { get; set; }

        public string UserId { get; set; }

    }
}
