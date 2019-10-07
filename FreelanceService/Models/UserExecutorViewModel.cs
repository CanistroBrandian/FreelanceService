using FreelanceService.BLL.DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FreelanceService.Web.Models
{
    public class UserExecutorViewModel
    {

        public string Id { get; set; }
        [Display(Name = "Почтовый ящик")]
        public string Email { get; set; }
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Display(Name = "Телефон")]
        public string Phone { get; set; }
        [Display(Name = "Местоположение")]
        public int City { get; set; }
        [Display(Name = "Рейтинг")]
        public decimal? Rating { get; set; }
        [Display(Name = "Роль")]
        public int Role { get; set; }
        [Display(Name = "Проекты")]
        public IEnumerable<ProjectDTO> projectDTOs {get;set; }
    }
}
