namespace FreelanceService.BLL.DTO
{
    public class UserRegistrationDTO
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string PassHash { get; set; }
        public string Phone { get; set; }
        public int City { get; set; }
        public int Role { get; set; }
        public string DynamicSalt { get; set; }
    }
}
