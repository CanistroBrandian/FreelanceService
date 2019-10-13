using FreelanceService.BLL.DTO;
using FreelanceService.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FreelanceService.Web.Models
{
    public class CreateJobViewModel
    {
        [Display(Name = ("Заголовок"))]
        public string Name { get; set; }
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
