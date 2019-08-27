using System;
using System.Collections.Generic;

namespace FreelanceService.DAL.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public int UserId_Executor { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int City { get; set; }
        public int Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishedDate { get; set; }
        public decimal? Price { get; set; }
    }
}
