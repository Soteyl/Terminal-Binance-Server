namespace Ixcent.CryptoTerminal.Application.Validation
{
    /// <summary>
    /// Validator for exchange tokens
    /// </summary>
    public interface IExchangeValidator
    {
        /// <summary>
        /// Validates an exchange token
        /// </summary>
        /// <param name="key">Token key</param>
        /// <param name="secret">Token secret</param>
        /// <returns>A cllection with names of accesses items</returns>
        Task<IEnumerable<string>> Validate(string key, string secret);
    }
}