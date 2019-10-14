using FreelanceService.Common.Enum;
using System.ComponentModel.DataAnnotations;

namespace FreelanceService.Web.Models
{
    public class ProfileEditViewModel
    {
        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(32, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 32 символов")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Не указана фамилия")]
        [StringLength(32, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 32 символов")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Не указан телефон")]
        [StringLength(12, MinimumLength = 9, ErrorMessage = "Длина строки должна быть от 9 до 12 символов")]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }
        [Display(Name = "Город")]
        public CityEnum City { get; set; }
    }
}
