using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceService.Common.Validation
{
    public interface IValidationService
    {
        IValidationResult Validate<T>(T model) where T : class;
    }
}
