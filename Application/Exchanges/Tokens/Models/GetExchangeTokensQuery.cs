using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Tokens.Models
{
    public class GetExchangeTokensQuery : IRequest<ExchangeTokensResult>
    {   }
}
