using System;

namespace FreelanceService.BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PassHash { get; set; }
        public string Phone { get; set; }
        public int City { get; set; }
        public DateTime RegistrationDateTime { get; set; }
        public decimal? Rating { get; set; }
        public int Role { get; set; }
        public string DynamicSalt { get; set; }
        public string VerifyCodeForResetPass { get; set; }
    }
}

