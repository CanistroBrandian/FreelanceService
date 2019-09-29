
using FreelanceService.Common.Validation;
using FreelanceService.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FreelanceService.Web.Validation.Validators
{
    public class ProfileEditViewModelValidator : IValidator<ProfileEditViewModel>
    {
        public IValidationResult Validate(ProfileEditViewModel model)
        {
            var result = new ViewModelValidatorValidationResult
            {
                ValidationResults = new List<ValidationResult>()
            };

            if (string.IsNullOrWhiteSpace(model.FirstName))
            {
                result.ValidationResults.Add(new ValidationResult("Введите имя!", new List<string>() { nameof(model.FirstName) }));
            }
            if (string.IsNullOrWhiteSpace(model.LastName))
            {
                result.ValidationResults.Add(new ValidationResult("Введите фамилию!", new List<string>() { nameof(model.LastName) }));
            }

            return result;
        }
    }
}
