using FreelanceService.Common.Validation;
using System;

namespace FreelanceService.BLL.Validation
{
    public class DTOValidationService : ValidationService, IDTOValidationService
    {
        public DTOValidationService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        ValidatorValidationResult IDTOValidationService.Validate<T>(T model)
        {
            var result = Validate<T>(model);
            return new ValidatorValidationResult
            {
                ValidationResults = result.ValidationResults
            };
        }
    }
}
