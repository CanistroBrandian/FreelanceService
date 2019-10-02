using FreelanceService.Common.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace FreelanceService.Web.Validation
{
    public class ViewModelValidatorValidationResult : ValidatorValidationResult
    {
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
    }
}
