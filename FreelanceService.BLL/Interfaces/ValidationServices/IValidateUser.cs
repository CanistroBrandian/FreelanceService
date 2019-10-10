using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceService.BLL.Interfaces.ValidationServices
{
    public interface IValidateUser
    {
        Task<bool> ValidateNewUser(string email, string phone, string firstName, string lastName);




    }
}
