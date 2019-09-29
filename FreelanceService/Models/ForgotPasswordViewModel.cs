using System.ComponentModel.DataAnnotations;

namespace FreelanceService.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
