using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceService.BLL.Models
{
    public class JobViewModel
    {
        [Display(Name = ("Заголовок"))]
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
