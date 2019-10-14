using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceService.BLL.Interfaces.ValidationServices
{
   public interface IValidateJob
    {
        bool ValidateNewJob(DateTime finishedDate, decimal? price);
        bool ValidateEditJob(DateTime finishedDate, decimal? price);
    }
}
