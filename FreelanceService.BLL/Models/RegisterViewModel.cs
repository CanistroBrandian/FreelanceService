using System.ComponentModel.DataAnnotations;

namespace FreelanceService.BLL.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Не указано Ваше Имя")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Не указана Ваша Фамилия")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Не указан Ваш Телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Не указан Ваш Город")]
        public int City { get; set; }
          [Required(ErrorMessage = "Не указана Ваша Роль")]
        public int Role { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }


    }
}
