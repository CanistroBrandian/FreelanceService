using FreelanceService.BLL.DTO;
using FreelanceService.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace FreelanceService.BLL.Interfaces
{
    public interface IUserService
    {
        Task AddUser(UserRegistrationDTO model);
        Task<UserDTO> FindUserByEmail(string email);
        Task<UserDTO> FindUserById(int id);
        Task<IEnumerable<UserDTO>> GetAll();
        Task Update(UserProfileEditDTO entity,UserDTO userDTO);
        Task CommitAsync();

    }
}
