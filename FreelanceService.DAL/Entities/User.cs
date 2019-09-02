using System;
using System.Collections.Generic;

namespace FreelanceService.DAL.Entities
{
   public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PassHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string DynamicSalt { get; set; }
        public DateTime DateRegistration { get; set; }
        public string Image { get; set; }
        public decimal? Rating { get; set; }
        public string Role { get; set; }

    }
}
