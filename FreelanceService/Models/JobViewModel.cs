using FreelanceService.BLL.DTO;
using FreelanceService.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FreelanceService.Web.Models
{
    public class JobViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }

        [Display(Name = "Категория")]
        public string CategoryName { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Статус")]
        public JobStatusEnum Status { get; set; }

        [Display(Name = "Город")]
        public CityEnum City { get; set; }

        [Display(Name = "Дата окончания")]
        public DateTime FinishedDateTime { get; set; }

        [Display(Name = "Цена")]
        public decimal? Price { get; set; }

    }
}
