using System.ComponentModel.DataAnnotations;

namespace FreelanceService.Web.Models
{
    public class ResetPasswordViewModel
    {
        [Required]
        [Display(Name = "Почтовый ящик")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Повторите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }

    }
}
