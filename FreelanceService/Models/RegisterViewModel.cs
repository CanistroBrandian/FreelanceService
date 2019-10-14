using FreelanceService.Common.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FreelanceService.Web.Models
{
    public class RegisterViewModel 
    {
        [Required(ErrorMessage = "Не указан почтовый ящик")]
        [Display(Name ="Почтовый ящик")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Не указано имя")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Не указана фамилия")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Не указан Телефон")]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }
        [Display(Name = "Город")]
        public CityEnum City { get; set; }
        [Display(Name = "Ваша роль")]
        public RoleEnum Role { get; set; }
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Повторите пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
              
    }
}
