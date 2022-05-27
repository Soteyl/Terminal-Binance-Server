namespace Ixcent.CryptoTerminal.Application.Validation
{
    using EFData;

    /// <summary>
    /// Validation factory for exchange tokens
    /// </summary>
    public class ExchangesValidator
    {
        public ExchangesValidatorByToken ByToken()
        {
            return new ExchangesValidatorByToken();
        }

        public ExchangesValidatorByUser ByUser(CryptoTerminalContext context, string userId)
        {
            return new ExchangesValidatorByUser(context, userId);
        }
    }
}
