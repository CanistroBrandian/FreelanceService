using FreelanceService.BLL.Interfaces.ValidationServices;
using FreelanceService.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceService.BLL.Services.Validations
{


    public class ValidateCreateUser : IValidateUser //проборосить логгер
    {
        IUnitOfWork _uow;
        public ValidateCreateUser (IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<bool> ValidateNewUser(string email, string phone, string firstName, string lastName)
        {

            var user = await _uow.UserRepos.FindByEmail(email);
            bool hasSameEmail = user.Email.Equals(null);
            bool hasSamePhone = user.Phone.Equals(null);

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

            if (!hasSamePhone)
            {
                //добавить строчку логера об ошибке чо такой пльзователь есть
                return false;

            }

            return true;
        }

        
    }
}
