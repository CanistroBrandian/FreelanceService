using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FreelanceService.BLL.Models
{
    public class RegisterViewModel :IValidatableObject
    {
        [Display(Name ="Почтовый ящик")]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Display(Name = "Телефон")]
        public string Phone { get; set; }
        [Display(Name = "Город")]
        public int City { get; set; }
        [Display(Name = "Ваша роль")]
        public int Role { get; set; }
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Повторите пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(this.FirstName))
            {
                errors.Add(new ValidationResult("Введите имя!", new List<string>() { "FirstName" }));
            }
            if (string.IsNullOrWhiteSpace(this.LastName))
            {
                errors.Add(new ValidationResult("Введите Фамилию!", new List<string>() { "LastName" }));
            }
            if (string.IsNullOrWhiteSpace(this.Email))
            {
                errors.Add(new ValidationResult("Введите электронный адрес!"));
            }
            if (this.Role != 1 || this.Role != 2)
            {
                errors.Add(new ValidationResult("Недопустимое значение роли"));
            }
            if (this.Phone.Length != 12)
            {
                errors.Add(new ValidationResult("Номер содержит 12 символов"));
            }
            if (this.City >=1 || this.City<=6)
            {
                errors.Add(new ValidationResult("Такого города не существует"));
            }

            return errors;
        }

    }
}
