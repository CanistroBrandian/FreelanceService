using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceService.Web.Models
{
    public class MyJobDetaiksViewModel
    {
        public int Id { get; set; }
        public int UserId_Customer { get; set; }
        public int UserId_Executor { get; set; }
        public string FirstNameCustomer { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int City { get; set; }
        public DateTime FinishedDateTime { get; set; }
        public decimal? Price { get; set; }
        public IEnumerable<ResponseListOfExecutors> ResponseListOfExecutors { get; set; }
    }
}
