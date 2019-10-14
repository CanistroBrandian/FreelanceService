﻿using FreelanceService.Common.Enum;
using System.ComponentModel.DataAnnotations;

namespace FreelanceService.Web.Models
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
        public CityEnum City { get; set; }
        [Display(Name = "Роль")]
        public RoleEnum Role { get; set; }
    }
}
