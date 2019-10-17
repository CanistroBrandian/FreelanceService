using FreelanceService.Common.Enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace FreelanceService.Web.Models
{
    public class ResponseDetailsViewModel
    {
        public int ResponseId { get; set; }
        [Display(Name = "Номер задания")]
        public int JobId { get; set; }

        [Display(Name = "Категория")]
        public int CategoryId { get; set; }

        [Display(Name = "Категория")]
        public string CategoryName { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Дата отклика")]
        public DateTime ResponseDateTime { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name="Название работы")]
        public string NameJob { get; set; }

        [Display(Name = "Статус работы")]
        public JobStatusEnum StatusJob { get; set; }
    }
}
