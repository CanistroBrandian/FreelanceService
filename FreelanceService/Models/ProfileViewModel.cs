using System.ComponentModel.DataAnnotations;

namespace FreelanceService.Models
{
    public  class ProfileViewModel
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
        public string City { get; set; }
        [Display(Name = "Роль")]
        public string Role { get; set; }
    }
}
