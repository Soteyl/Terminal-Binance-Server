using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixcent.CryptoTerminal.Domain.Database.Models
{
    public class ExchangeToken
    {
        public int UserId { get; set; }

        public string Secret { get; set; }

        public string Key { get; set; }

        public string Exchange { get; set; }

    }
}
