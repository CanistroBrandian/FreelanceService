using FreelanceService.Common.Validation;
using FreelanceService.Web.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace FreelanceService.Web.Validation.Validators
{
    public class RegisterViewModelValidator : IValidator<RegisterViewModel>
    {
        public IValidationResult Validate(RegisterViewModel model)
        {
            var result = new ViewModelValidatorValidationResult
            {
                ValidationResults = new List<ValidationResult>()
            };

            if (string.IsNullOrWhiteSpace(model.Email))
                result.ValidationResults.Add(new ValidationResult("Введите Email", new List<string>() { nameof(model.Email) }));

            if (string.IsNullOrWhiteSpace(model.FirstName))
                result.ValidationResults.Add(new ValidationResult("Введите Имя!", new List<string>() { nameof(model.FirstName) }));

            if (string.IsNullOrWhiteSpace(model.LastName))
                result.ValidationResults.Add(new ValidationResult("Введите фамилию!", new List<string>() { nameof(model.LastName) }));

            if (string.IsNullOrWhiteSpace(model.Phone))
                result.ValidationResults.Add(new ValidationResult("Введите Номер Телефона!", new List<string>() { nameof(model.Phone) }));

            if (model.ConfirmPassword != model.Password)
                result.ValidationResults.Add(new ValidationResult("Пароли не совпадают", new List<string>() { nameof(model.ConfirmPassword) }));

            if (string.IsNullOrWhiteSpace(model.Phone))
                result.ValidationResults.Add(new ValidationResult("Введите номер телефона", new List<string>() { nameof(model.Phone) }));
           else if (model.Phone.Length <= 9 && model.Phone.Length <= 12)
                result.ValidationResults.Add(new ValidationResult("Длина номера телефона должна состовлять от 9-ти до 12ти символов", new List<string>() { nameof(model.Phone) }));

            try
            {
                var checkMail = new MailAddress(model.Email);
                if (checkMail.Address == null)
                    result.ValidationResults.Add(new ValidationResult("Введите корректный Email", new List<string>() { nameof(model.Email) }));
            }
            catch { result.ValidationResults.Add(new ValidationResult("Введите корректный Email", new List<string>() { nameof(model.Email) })); }
            return result;
        }
    }
}
