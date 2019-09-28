using System;

namespace FreelanceService.Common.Validation
{
    public abstract class ValidationService
    {
        private readonly IServiceProvider _serviceProvider;
        public ValidationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IValidationResult Validate<T>(T model) where T : class
        {
            var modelValidator = ((IValidator<T>)_serviceProvider.GetService(typeof(IValidator<T>)));
            if (modelValidator == null)
            {
                throw new NullReferenceException($"No validator found for model of type {nameof(T)}");
            }
            return modelValidator.Validate(model);
        }
    }
}
