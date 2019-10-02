using System;

namespace FreelanceService.BLL.DTO
{
    public class JobDTO
    {
        public int Id { get; set; }
        public int UserId_Executor { get; set; }
        public int UserId_Customer { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int City { get; set; }
        public int Status { get; set; }
        public DateTime RegistrationJobDateTime { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime FinishedDateTime { get; set; }
        public decimal? Price { get; set; }
    }
}
