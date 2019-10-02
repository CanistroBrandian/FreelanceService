using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FreelanceService.Common.Validation
{
    public class ValidatorValidationResult : IValidationResult
    {
        public List<ValidationResult> ValidationResults { get; set; } = new List<ValidationResult>();

        public bool IsValid => !ValidationResults.Any(f => !string.IsNullOrEmpty(f.ErrorMessage));

        public string ErrorMessage =>
             ValidationResults.Where(f => !string.IsNullOrEmpty(f.ErrorMessage)).FirstOrDefault()?.ErrorMessage;
    }
}
