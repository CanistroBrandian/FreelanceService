using FreelanceService.Common.Validation;
using System;

namespace FreelanceService.Web.Validation
{
    public class ViewModelValidationService : IViewModelValidationService
    {
        private readonly IServiceProvider _serviceProvider;
        public ViewModelValidationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IValidationResult Validate<T>(T model) where T : class
        {
            var modelValidator = ((IValidator<T>)_serviceProvider.GetService(typeof(IValidator<T>)));
            if(modelValidator == null)
            {
                throw new NullReferenceException($"No validator found for model of type {nameof(T)}");
            }
            return modelValidator.Validate(model);
        }

        ViewModelValidatorValidationResult IViewModelValidationService.Validate<T>(T model)
        {
            var result = Validate<T>(model);
            return new ViewModelValidatorValidationResult
            {
                ValidationResults = result.ValidationResults
            };
        }
    }
}
