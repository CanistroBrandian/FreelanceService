using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceService.Web.Models
{
    public class ResponseDetailsViewModel
    {
        public int ResponseId { get; set; }
        public int JobId { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public DateTime ResponseDateTime { get; set; }
        public decimal Price { get; set; }
        public string NameJob { get; set; }
    }
}
