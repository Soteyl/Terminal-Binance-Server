using Ixcent.CryptoTerminal.Domain.Database.Models;

namespace Ixcent.CryptoTerminal.Application.Validation
{
    /// <summary>
    /// Result of validation for Exchange tokens
    /// </summary>
    public class TokenValidationResult
    {
        public bool IsSuccess { get; set; }

        public object? Errors { get; set; }

        public ExchangeToken? ValidatedToken { get; set; }

        public static TokenValidationResult Success(ExchangeToken? token)
            => new TokenValidationResult { ValidatedToken = token, IsSuccess = true };

        public static TokenValidationResult Error(ExchangeToken? token, object? errors)
            => new TokenValidationResult { ValidatedToken = token, IsSuccess = false, Errors = errors };
    }
}