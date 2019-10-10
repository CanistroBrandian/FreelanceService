using FreelanceService.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceService.BLL.Services.Validations
{
    public class ValidateCreateJob
    {
        IUnitOfWork _uow;

        public ValidateCreateJob(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<bool> ValidateNewUser(string email, string phone, string firstName, string lastName)
        {

            var user = await _uow.UserRepos.FindByEmail(email);
            var phoneOfUser = user.Phone;
            bool hasSameEmail = user.Email.Equals(null);


            if (String.IsNullOrWhiteSpace(email))
            {
                // добавляем строчку лога для exception
                return false;
            }

            if (String.IsNullOrWhiteSpace(firstName))
            {
                return false;
            }

            if (String.IsNullOrWhiteSpace(lastName))
            {
                return false;
            }

            if (!hasSameEmail)
            {
                //добавить строчку логера об ошибке чо такой пльзователь есть
                return false;

            }

            if (phoneOfUser == phone)
            {
                //добавить строчку логера об ошибке чо такой пльзователь есть
                return false;

            }

            return true;
        }
    }
}
