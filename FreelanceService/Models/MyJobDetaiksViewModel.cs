using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceService.Web.Models
{
    public class MyJobDetailsViewModel
    {
        [DisplayName("Номер Задания")]
        public int Id { get; set; }
        public int UserId_Customer { get; set; }
        public int UserId_Executor { get; set; }
        public string FirstNameCustomer { get; set; }
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }
        [Display(Name = "Название задания")]
        public string Name { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Местоположение")]
        public int City { get; set; }
        [Display(Name = "Статус задачи")]
        public int Status { get; set; }
        [Display(Name = "Время окончания")]
        public DateTime FinishedDateTime { get; set; }
        [Display(Name = "Цена")]
        public decimal? Price { get; set; }
        [Display(Name = "Отклики")]
        public IEnumerable<ResponseListOfExecutors> ResponseListOfExecutors { get; set; }
    }
}
