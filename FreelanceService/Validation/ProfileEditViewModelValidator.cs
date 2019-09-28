using FreelanceService.BLL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceService.Web.Validation
{
    public class ProfileEditViewModelValidator : IValidator<ProfileEditViewModel>
    {
        public ValidatorValidationResult Validate(ProfileEditViewModel model)
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
            //if (this.Age < 0 || this.Age > 120)
            //{
            //    result.ValidationResults.Add(new ValidationResult("Недопустимый возраст!"));
            //}

            return result;
        }
    }
}
