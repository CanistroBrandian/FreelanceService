using FreelanceService.BLL.Interfaces.ValidationServices;
using System;

namespace FreelanceService.BLL.Services.Validations
{
    public class ValidateJob :IValidateJob
    {
        public bool ValidateEditJob(DateTime finishedDate, decimal? price)
        {
            var dateToDay = DateTime.Today;


            if (finishedDate < dateToDay)
            {
                return false;
            }

            if (price < 0)
            {
                return false;
            }

            return true;
        }

        public bool ValidateNewJob(DateTime finishedDate, decimal? price)
        {
            var dateToDay = DateTime.Today;

            if (finishedDate < dateToDay)
            {
                return false;
            }

            if (price < 0)
            {
                return false;
            }

            return true;
        }
    }
}
