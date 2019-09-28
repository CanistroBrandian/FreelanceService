using System;

namespace FreelanceService.Web.Validation
{
    public class ViewModelValidationService : IValidationService
    {
        private readonly IServiceProvider _serviceProvider;
        public ViewModelValidationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public ValidatorValidationResult Validate<T>(T model) where T : class
        {
            return ((IValidator<T>)_serviceProvider.GetService(typeof(IValidator<T>))).Validate(model);
        }
    }
}
