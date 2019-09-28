using FreelanceService.Common.Validation;
using System;

namespace FreelanceService.Web.Validation
{
    public class ViewModelValidationService : ValidationService, IViewModelValidationService
    {
        public ViewModelValidationService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
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
