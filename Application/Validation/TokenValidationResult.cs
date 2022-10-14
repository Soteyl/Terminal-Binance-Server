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

        public ExchangeTokenEntity? ValidatedToken { get; set; }

        public static TokenValidationResult Success(ExchangeTokenEntity? token) => new() { ValidatedToken = token, IsSuccess = true };

        public static TokenValidationResult Error(ExchangeTokenEntity? token, object? errors) => new() { ValidatedToken = token, IsSuccess = false, Errors = errors };
    }
}