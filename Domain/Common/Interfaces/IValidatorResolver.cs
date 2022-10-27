using FluentValidation.Results;

namespace Ixcent.CryptoTerminal.Domain.Common.Interfaces
{
    public interface IValidatorResolver
    {
        /// <summary>
        /// Validates the specified instance.
        /// </summary>
        /// <param name="instance">The instance to validate</param>
        /// <returns>A ValidationResult object containing any validation failures.</returns>
        ValidationResult Validate<T>(T instance);

        /// <summary>
        /// Validate the specified instance asynchronously
        /// </summary>
        /// <param name="instance">The instance to validate</param>
        /// <param name="cancellation"></param>
        /// <returns>A ValidationResult object containing any validation failures.</returns>
        Task<ValidationResult> ValidateAsync<T>(T instance, CancellationToken cancellation = default);
    }
}