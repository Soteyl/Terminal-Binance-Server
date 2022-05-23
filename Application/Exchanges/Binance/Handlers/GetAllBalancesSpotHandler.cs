using MediatR;
using Binance.Net;
using Binance.Net.Objects.Spot.SpotData;

namespace Ixcent.CryptoTerminal.Application.Exchanges.Binance.Handlers
{
    using CryptoExchange.Net.Objects;
    using Ixcent.CryptoTerminal.Application.Exceptions;
    using Ixcent.CryptoTerminal.Application.Validation;
    using Ixcent.CryptoTerminal.EFData;
    using Microsoft.AspNetCore.Http;
    using Models;

    public class GetAllBalancesSpotHandler : IRequestHandler<GetAllBalancesSpotModel, IEnumerable<BinanceBalance>>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        private readonly CryptoTerminalContext _context;

        private readonly ExchangesValidator _validator;

        public GetAllBalancesSpotHandler(IHttpContextAccessor contextAccessor, CryptoTerminalContext context, ExchangesValidator validator)
        {
            _context = context;
            _contextAccessor = contextAccessor;
            _validator = validator;
        }

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
