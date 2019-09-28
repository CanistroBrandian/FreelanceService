using FreelanceService.Common.Validation;

namespace FreelanceService.BLL.Validation
{
    public interface IDTOValidationService : IValidationService
    {
        new ValidatorValidationResult Validate<T>(T model) where T : class;
    }
}
