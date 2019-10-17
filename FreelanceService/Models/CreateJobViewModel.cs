using FreelanceService.BLL.DTO;
using FreelanceService.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FreelanceService.Web.Models
{
    public class CreateJobViewModel
    {
        [Required(ErrorMessage = "Не указан заголовок")]
        [StringLength(128, MinimumLength = 5, ErrorMessage = "Длина строки должна быть от 5 до 128 символов")]
        [Display(Name = ("Заголовок"))]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указано описание")]
        [StringLength(1024, MinimumLength = 10, ErrorMessage = "Длина строки должна быть от 10 до 1024 символов")]
        [Display(Name = ("Описание"))]
        public string Description { get; set; }

        [Display(Name = ("Категория"))]
        public int CategoryId { get; set; }

        [Display(Name = ("Город"))]
        public CityEnum City { get; set; }

        [Display(Name = ("Дата выполнения"))]
        public DateTime FinishedDateTime { get; set; }

        [Display(Name = ("Цена"))]
        public decimal? Price { get; set; }
        public IEnumerable<CategoryDTO> CategoryDTOs { get; set; }

    }
}
