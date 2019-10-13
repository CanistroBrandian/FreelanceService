using FreelanceService.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceService.Web.Models
{
    public class JobEditViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CityEnum City { get; set; }
        public DateTime FinishedDateTime { get; set; }
        public decimal? Price { get; set; }
    }
}
