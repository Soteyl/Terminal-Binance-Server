using MediatR;
using Binance.Net;
using Binance.Net.Objects.Spot.SpotData;
using CryptoExchange.Net.Objects;
using Microsoft.AspNetCore.Http;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Handlers
{
    using Domain.Database.Models;
    using Binance.Models;
    using Exceptions;
    using Validation;
    using EFData;

    /// <summary>
    /// Get all spot balances handler. Allows to get all cryptocurrency balances from the Binance.<br/>
    /// Implements: <see cref="IRequestHandler{GetAllBalancesSpotModel, IEnumerable{BinanceBalance}"/><br/>
    /// Is used by: MediatR
    /// </summary>
    public class GetAllBalancesSpotHandler : IRequestHandler<GetAllBalancesSpotModel, IEnumerable<BinanceBalance>>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        private readonly CryptoTerminalContext _context;

        private readonly ExchangesValidator _validator;
        /// <summary>
        /// Constructor for <see cref="GetAllBalancesSpotHandler"/>.
        /// All the parameters in the contructor provided by the dependency injection.
        /// </summary>
        /// <param name="contextAccessor"> Context accessor which is required to get information about user. </param>
        /// <param name="context"> Allows to access tables in CryptoTerminal database. Required to access <see cref="ExchangeToken"/> for Binance. </param>
        /// <param name="validator"> Validates whether provided token for Binance is valid or not.</param>
        public GetAllBalancesSpotHandler(IHttpContextAccessor contextAccessor, CryptoTerminalContext context, ExchangesValidator validator)
        {
            _context = context;
            _contextAccessor = contextAccessor;
            _validator = validator;
        }
        /// <summary>
        /// Main method 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="RestException"></exception>
        public async Task<IEnumerable<BinanceBalance>> Handle(GetAllBalancesSpotModel request, CancellationToken cancellationToken)
        {
            BinanceClient client = new BinanceClient();

            var token = _context.ExchangeTokens.FirstOrDefault(t => t.Exchange == "Binance" && t.UserId == _contextAccessor.GetCurrentUserId());

            if (token == null)
                throw new RestException(System.Net.HttpStatusCode.BadRequest, new
                {
                    Token = "Failed to get API token for specified parameters."
                });

            string key = token.Key;
            string secret = token.Secret;

            bool isValid = _validator.Validate(key, secret, "Binance").Result.Contains("spot");

            if (!isValid)
                throw new RestException(System.Net.HttpStatusCode.BadRequest, new
                {
                    Token = "Passed user API token is outdated!"
                });

            client.SetApiCredentials(key, secret);

            WebCallResult<BinanceAccountInfo> info = await client.General.GetAccountInfoAsync();

            IEnumerable<BinanceBalance> balances = info.Data.Balances;

            return balances;
        }
    }
}
