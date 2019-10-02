using System.ComponentModel.DataAnnotations;

namespace FreelanceService.Web.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
