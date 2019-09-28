using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceService.Web.Validation
{
    public interface IValidator<T> where T : class
    {
        ValidatorValidationResult Validate(T model);
    }
}
