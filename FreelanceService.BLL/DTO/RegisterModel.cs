using System.ComponentModel.DataAnnotations;

namespace FreelanceService.BLL.DTO
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Не указано Ваше Имя")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Не указана Ваша Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }

        public string Phone { get; set; }

    }
}
