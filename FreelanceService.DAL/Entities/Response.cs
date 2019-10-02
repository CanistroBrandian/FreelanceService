using System;
using System.Collections.Generic;
using System.Text;

namespace FreelanceService.DAL.Entities
{
    public class Response
    {
        public int Id { get; set; }
        public int UserId_Executor { get; set; }
        public int JobId { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public DateTime ResponseDateTime { get; set; } 
        public decimal? Price { get; set; }
    }
}
