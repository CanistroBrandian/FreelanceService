using FreelanceService.BLL.DTO;
using FreelanceService.Common.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FreelanceService.BLL.Validation.Validator
{
    public class UserProfileEditDTOValidator : IValidator<UserProfileEditDTO>
    {
        public IValidationResult Validate(UserProfileEditDTO model)
        {
            var result = new ValidatorValidationResult
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
            if (string.IsNullOrWhiteSpace(model.Email))
            {
                result.ValidationResults.Add(new ValidationResult("Введите электронный адрес!", new List<string>() { nameof(model.Email) }));
            }

            return result;
        }
    }
}
