using System.ComponentModel.DataAnnotations;

namespace FreelanceService.Web.Models
{
    public class ResponseEditViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
    }
}
