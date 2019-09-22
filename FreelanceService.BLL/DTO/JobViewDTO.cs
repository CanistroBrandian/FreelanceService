using System;

namespace FreelanceService.BLL.DTO
{
    public class JobViewDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int City { get; set; }
        public DateTime FinishedDateTime { get; set; }
        public decimal? Price { get; set; }
    }
}
