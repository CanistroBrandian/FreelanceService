using FreelanceService.BLL.Interfaces.ValidationServices;
using System;

namespace FreelanceService.Web.Validations
{
    public class ValidateJob :IValidateJob
    {
        public bool ValidateEditJob(string name, string description, DateTime finishedDate, decimal? price)
        {
            var dateToDay = DateTime.Today;

            if (String.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            if (String.IsNullOrWhiteSpace(description))
            {
                return false;
            }

            if (finishedDate >= dateToDay)
            {
                return false;
            }

            if (price >= 0)
            {
                return false;
            }

            return true;
        }

        public bool ValidateNewJob(string name, string description, DateTime finishedDate, decimal? price)
        {
            var dateToDay = DateTime.Today;
            if (String.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            if (String.IsNullOrWhiteSpace(description))
            {
                return false;
            }

            if (finishedDate <= dateToDay)
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
