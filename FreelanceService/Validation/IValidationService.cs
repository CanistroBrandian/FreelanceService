using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceService.Web.Validation
{
    public interface IValidationService
    {
        ValidatorValidationResult Validate<T>(T model) where T : class;
    }
}
