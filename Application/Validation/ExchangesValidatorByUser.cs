using Ixcent.CryptoTerminal.EFData;

namespace Ixcent.CryptoTerminal.Application.Validation
{
    /// <summary>
    /// Factory for validating exchange tokens by a user and a database
    /// </summary>
    public class ExchangesValidatorByUser
    {
        protected readonly CryptoTerminalContext _context;

        protected HashSet<string> _requiredPoints = new HashSet<string>();

        protected readonly string _userId;

        public IEnumerable<string> RequiredPoints => _requiredPoints;

        internal ExchangesValidatorByUser(CryptoTerminalContext context, string userId)
        {
            _context = context;
            _userId = userId;
        }

        /// <summary>
        /// If there is no method with accessing to needed validator, 
        /// this method gives access to find needed validator by it's name
        /// </summary>
        /// <param name="exchangeName">Name of exchange</param>
        /// <param name="requiredPoints">Names of accessed parts of exchange that need to validate</param>
        /// <returns></returns>
        public ExchangeTokenValidatorChoosedExchange OtherExchange(string exchangeName,
                                                                       IEnumerable<string> requiredPoints)
        {
            return new ExchangeTokenValidatorChoosedExchange(_context, _userId, exchangeName)
            {
                _requiredPoints = new HashSet<string>(requiredPoints)
            };
        }

        /// <summary>
        /// Get a Binance validator
        /// </summary>
        public BinanceTokenValidator Binance()
        {
            return new BinanceTokenValidator(_context, _userId);
        }
    }
}