using Binance.Net.Objects.Spot.SpotData;
using CryptoExchange.Net.Objects;
using Microsoft.AspNetCore.Http;
using Binance.Net;
using MediatR;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Spot.Handlers
{
    using Domain.Database.Models;
    using Validation;
    using Exceptions;
    using Results;
    using Models;
    using EFData;

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

        private readonly ExchangesValidator _validator;
        /// <summary>
        /// Constructor for <see cref="GetAllBalancesHandler"/>.
        /// All the parameters in the contructor provided by the dependency injection.
        /// </summary>
        /// <param name="contextAccessor"> Context accessor which is required to get information about user. </param>
        /// <param name="context"> Allows to access tables in CryptoTerminal database. Required to access <see cref="ExchangeToken"/> for Binance. </param>
        /// <param name="validator"> Validates whether provided token for Binance is valid or not.</param>
        public GetAllBalancesHandler(IHttpContextAccessor contextAccessor, CryptoTerminalContext context, ExchangesValidator validator)
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
        public async Task<GetAllBalancesResult> Handle(GetAllBalancesModel request, CancellationToken cancellationToken)
        {
            BinanceClient client = new BinanceClient();

            string exchangeName = "Binance";
            string userId = _contextAccessor.GetCurrentUserId();

            var token = _context.ExchangeTokens.FirstOrDefault(t => t.Exchange == exchangeName && t.UserId == userId);

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

            return new GetAllBalancesResult
            {
                AvailableBalances = balances
            };
        }
    }
}
