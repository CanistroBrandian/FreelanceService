using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceService.Models
{
    public class JobDetailsViewModel
    {
        public int Id { get; set; }
        public int UserId_Customer { get; set; }
        public string FrstName { get; set; }
        public string LastName { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DescriptionForResponse { get; set; }
        public int City { get; set; }
        public DateTime FinishedDateTime { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceForResponse { get; set; }
        public ResponseAddViewModel ResponseAddViewModel { get; set; }
    }
}
