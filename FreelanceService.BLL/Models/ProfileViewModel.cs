using System;

namespace FreelanceService.BLL.Models
{
    public  class ProfileViewModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int City { get; set; }
        public int Role { get; set; }
    }
}
