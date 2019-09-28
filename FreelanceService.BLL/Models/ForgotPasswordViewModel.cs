using System.ComponentModel.DataAnnotations;

namespace FreelanceService.BLL.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
