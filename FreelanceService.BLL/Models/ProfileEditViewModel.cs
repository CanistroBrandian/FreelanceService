using System.ComponentModel.DataAnnotations;

namespace FreelanceService.BLL.Models
{
    public class ProfileEditViewModel
    {
        [Display(Name = "Почтовый ящик")]
        public string Email { get; set; }
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Display(Name = "Телефон")]
        public string Phone { get; set; }
        [Display(Name = "Город")]
        public int City { get; set; }
        [Display(Name = "Роль")]
        public int Role { get; set; }
    }
}
