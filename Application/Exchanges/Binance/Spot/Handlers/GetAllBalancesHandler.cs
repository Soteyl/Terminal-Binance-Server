using Binance.Net;
using Binance.Net.Objects.Spot.SpotData;

using CryptoExchange.Net.Objects;

using Ixcent.CryptoTerminal.Application.Exceptions;
using Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Models;
using Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Results;
using Ixcent.CryptoTerminal.EFData;

using MediatR;

using Microsoft.AspNetCore.Http;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Handlers
{
    /// <summary>
    /// Get all spot balances handler. Allows to get all cryptocurrency balances from the Binance.
    /// </summary>
    /// <remarks>
    /// Implements: <see cref="IRequestHandler{TRequest, TResponse}"/><br/>
    /// <c>TRequest</c> is <see cref="AllBalancesModel"/><br/>
    /// <c>TResponse</c> is <see cref="GetAllBalancesResult"/><br/>
    /// Is used by: MediatR
    /// </remarks>
    public class GetAllBalancesHandler : IRequestHandler<AllBalancesModel, GetAllBalancesResult>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        private readonly CryptoTerminalContext _context;

        /// <summary>
        /// Constructor for <see cref="GetAllBalancesHandler"/>.
        /// All the parameters in the contructor provided by the dependency injection.
        /// </summary>
        /// <param name="contextAccessor"> Context accessor which is required to get information about user. </param>
        /// <param name="context"> Allows to access tables in CryptoTerminal database. Required to access <see cref="ExchangeToken"/> for Binance. </param>
        public GetAllBalancesHandler(IHttpContextAccessor contextAccessor, CryptoTerminalContext context)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        /// <summary> Main method </summary>
        /// <exception cref="RestException"></exception>
        public async Task<GetAllBalancesResult> Handle(AllBalancesModel request, CancellationToken cancellationToken)
        {
            BinanceClient client = new BinanceClient();

            string userId = _contextAccessor.GetCurrentUserId()!;

            var token = _context.ExchangeTokens.FirstOrDefault(token => token.UserId == userId &&
                                                                        token.Exchange.Equals("Binance"));

            if (token == null)
                throw RestException.MissingApiToken;


            client.SetApiCredentials(token.Key, token.Secret);

            WebCallResult<BinanceAccountInfo> info = await client.General.GetAccountInfoAsync(ct: CancellationToken.None);

            info.RemoveTokenAndThrowRestIfInvalid(_context, token);

            IEnumerable<BinanceBalance> balances = info.Data.Balances;

            return new GetAllBalancesResult
            {
                AvailableBalances = balances
            };
        }
    }
}
