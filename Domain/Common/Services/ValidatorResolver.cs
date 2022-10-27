using FluentValidation;
using FluentValidation.Results;

using Ixcent.CryptoTerminal.Domain.Common.Interfaces;

using Microsoft.Extensions.DependencyInjection;

namespace Ixcent.CryptoTerminal.Domain.Common.Services
{
    public class ValidatorResolver: IValidatorResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidatorResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public ValidationResult Validate<T>(T instance)
        {
            return _serviceProvider.GetRequiredService<IValidator<T>>().Validate(instance);
        }

        public async Task<ValidationResult> ValidateAsync<T>(T instance, CancellationToken cancellation = default)
        {
            return await _serviceProvider.GetRequiredService<IValidator<T>>().ValidateAsync(instance, cancellation);
        }
    }
}