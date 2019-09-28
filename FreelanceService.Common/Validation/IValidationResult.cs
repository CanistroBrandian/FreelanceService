using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FreelanceService.Common.Validation
{
    public interface IValidationResult
    {
        List<ValidationResult> ValidationResults { get; set; }

        bool IsValid { get; }

        string ErrorMessage { get; }
    }
}
