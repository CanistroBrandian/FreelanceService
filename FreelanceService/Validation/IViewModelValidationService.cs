using FreelanceService.Common.Validation;

namespace FreelanceService.Web.Validation
{
    public interface IViewModelValidationService : IValidationService
    {
        new ViewModelValidatorValidationResult Validate<T>(T model) where T : class;
    }
}
