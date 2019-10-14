using FreelanceService.BLL.DTO;
using System;
using System.Collections.Generic;

namespace FreelanceService.Web.Models
{
    public class ResponseListOfExecutors
    {
        public int ResponseId { get; set; }
        public int JobId { get; set; }
        public string UserId_Executor{ get; set; }
        public string FirstNameExecutor { get; set; }
        public string LastNameExecutor { get; set; }
        public string Description { get; set; }
        public DateTime ResponseDateTime { get; set; }
        public decimal? Price { get; set; }

    }
}
