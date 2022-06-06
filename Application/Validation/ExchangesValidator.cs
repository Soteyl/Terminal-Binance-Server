using Ixcent.CryptoTerminal.EFData;

namespace Ixcent.CryptoTerminal.Application.Validation
{
    /// <summary>
    /// Validation factory for exchange tokens
    /// </summary>
    public static class ExchangesValidator
    {
        public static ExchangesValidatorByToken ByToken()
        {
            return new ExchangesValidatorByToken();
        }

        public static ExchangesValidatorByUser ByUser(CryptoTerminalContext context, string userId)
        {
            return new ExchangesValidatorByUser(context, userId);
        }
    }
}
