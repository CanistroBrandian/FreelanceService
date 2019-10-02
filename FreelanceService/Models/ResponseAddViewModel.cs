using System.ComponentModel.DataAnnotations;

namespace FreelanceService.Web.Models
{
    public class ResponseAddViewModel
    {

        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
    }
}
