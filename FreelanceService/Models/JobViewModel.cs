using FreelanceService.BLL.DTO;
using FreelanceService.Common.Enum;
using System;

namespace FreelanceService.Web.Models
{
    public class JobViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public JobStatusEnum Status { get; set; }
        public CityEnum City { get; set; }
        public DateTime FinishedDateTime { get; set; }
        public decimal? Price { get; set; }
        public CategoryDTO Category { get; set; }
    }
}
