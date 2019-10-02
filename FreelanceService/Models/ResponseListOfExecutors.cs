using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceService.Web.Models
{
    public class ResponseListOfExecutors
    {

        public string FirstNameExecutor { get; set; }
        public string LastNameExecutor { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

    }
}
