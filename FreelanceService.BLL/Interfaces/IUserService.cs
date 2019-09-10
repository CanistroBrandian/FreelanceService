using FreelanceService.BLL.DTO;
using FreelanceService.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace FreelanceService.BLL.Interfaces
{
   public interface IUserService
    {
        Task AddUser(UserDTO entity);
        Task<UserDTO> FindUserByEmail(string email);
        Task<UserDTO> FindUserById(int id);
        Task<IEnumerable<UserDTO>> GetAll();
        Task CommitAsync();

    }
}
