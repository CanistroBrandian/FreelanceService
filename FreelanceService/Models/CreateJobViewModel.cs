using System;
using System.ComponentModel.DataAnnotations;

namespace FreelanceService.Models
{
    public class CreateJobViewModel
    {
        [Display(Name=("Заголовок"))]
        public string Name { get; set; }
        [Display(Name = ("Описание"))]
        public string Description { get; set; }
        [Display(Name = ("Категория"))]
        public int CategoryId { get; set; }
        [Display(Name = ("Город"))]
        public int City { get; set; }
        [Display(Name = ("Дата выполнения"))]
        public DateTime FinishedDateTime { get; set; }
        [Display(Name = ("Цена"))]
        public decimal? Price { get; set; }
        
    }
}
