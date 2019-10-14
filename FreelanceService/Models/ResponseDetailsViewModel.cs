using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceService.Web.Models
{
    public class ResponseDetailsViewModel
    {
        public int ResponseId { get; set; }
        public int JobId { get; set; }
        [Display(Name = "Катеория")]
        public int CategoryId { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Дата отклика")]
        public DateTime ResponseDateTime { get; set; }
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
        [Display(Name="Название работы")]
        public string NameJob { get; set; }
    }
}
