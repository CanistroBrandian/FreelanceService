using FreelanceService.BLL.DTO;
using FreelanceService.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceService.BLL.Interfaces
{
   public interface IProfileService
    {
        Task<ProfileViewModel> GetProfile(UserDTO user);
    }
}
