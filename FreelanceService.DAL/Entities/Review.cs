using System;
using System.Collections.Generic;
using System.Text;

namespace FreelanceService.DAL.Entities
{
   public class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateOfWriting { get; set; }
        public bool Feedback { get; set; }
        public decimal Rating { get; set; }
    }
}
