using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FreelanceService.Web.Validation
{
    public class ValidatorValidationResult
    {
        public List<ValidationResult> ValidationResults { get; set; } = new List<ValidationResult>();

        public bool IsValid => !ValidationResults.Any(f => !string.IsNullOrEmpty(f.ErrorMessage));

        public ModelStateDictionary ModelState
        {
            get
            {
                var modelState = new ModelStateDictionary();
                foreach (var result in ValidationResults)
                {
                    modelState.AddModelError(result.MemberNames.First(), result.ErrorMessage);
                }
                return modelState;
            }
        }
        public string ErrorMessage =>
             ValidationResults.Where(f => !string.IsNullOrEmpty(f.ErrorMessage)).FirstOrDefault()?.ErrorMessage;
    }
}
