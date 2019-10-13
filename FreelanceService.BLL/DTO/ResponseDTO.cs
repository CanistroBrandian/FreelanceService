using System;

namespace FreelanceService.BLL.DTO
{
    public class ResponseDTO
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public DateTime ResponseDateTime { get; set; }
        public decimal Price { get; set; }
        public UserDTO Executor { get; set; }
    }
}
