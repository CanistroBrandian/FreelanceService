using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FreelanceService.BLL.DTO
{
    public class UserDTO 
    {

        public int Id { get; set; }
        [Required]
        [Display(Name = "Почтовый ящик")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Пароль")]
        public string PassHash { get; set; }
        [Required]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }
        [Required]
        [Display(Name = "Город")]
        public int City { get; set; }
        public DateTime RegistrationDateTime { get; set; }
        public decimal? Rating { get; set; }
        [Required]
        [Display(Name = "Роль")]
        public int Role { get; set; }
        [Required]
        public string DynamicSalt { get; set; }




    }
}

