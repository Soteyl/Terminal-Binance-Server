using Binance.Net.Objects.Spot.SpotData;
using CryptoExchange.Net.Objects;
using Microsoft.AspNetCore.Http;
using Binance.Net;
using MediatR;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Handlers
{
    using Domain.Database.Models;
    using Exceptions;
    using EFData;
    using Models;
    using Results;

    /// <summary>
    /// Get all spot balances handler. Allows to get all cryptocurrency balances from the Binance.
    /// </summary>
    /// <remarks>
    /// Implements: <see cref="IRequestHandler{TRequest, TResponse}"/><br/>
    /// <c>TRequest</c> is <see cref="GetAllBalancesModel"/><br/>
    /// <c>TResponse</c> is <see cref="GetAllBalancesResult"/><br/>
    /// Is used by: MediatR
    /// </remarks>
    public class GetAllBalancesHandler : IRequestHandler<GetAllBalancesModel, GetAllBalancesResult>
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
        public async Task<GetAllBalancesResult> Handle(GetAllBalancesModel request, CancellationToken cancellationToken)
        {
            BinanceClient client = new BinanceClient();

            string userId = _contextAccessor.GetCurrentUserId()!;

            var token = _context.ExchangeTokens.FirstOrDefault(token => token.UserId == userId &&
                                                                        token.Exchange.Equals("Binance"));

            if (token == null)
                throw new RestException(System.Net.HttpStatusCode.BadRequest,
                                        ErrorCode.BadExchangeToken,
                                        new { Token = "Missing API token" });
            
            client.SetApiCredentials(token.Key, token.Secret);

            WebCallResult<BinanceAccountInfo> info = await client.General.GetAccountInfoAsync();

            info.RemoveTokenAndThrowRestIfInvalid(_context, token);

            IEnumerable<BinanceBalance> balances = info.Data.Balances;

            return new GetAllBalancesResult
            {
                AvailableBalances = balances
            };
        }
    }
}
