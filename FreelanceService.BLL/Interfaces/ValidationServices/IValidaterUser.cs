using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceService.BLL.Interfaces.ValidationServices
{
    public interface IValidaterUser
    {
        Task<bool> ValidateNewUser(string email, string phone);
        Task<bool> ValidateEditUser(string phone);
    }
}
