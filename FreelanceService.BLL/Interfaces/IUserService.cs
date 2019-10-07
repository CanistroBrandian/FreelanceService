using FreelanceService.BLL.DTO;
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
        Task<IEnumerable<UserDTO>> GetAllExecutor();
        Task<IEnumerable<UserDTO>> GetAllUsersExecutorsOfResponse(int jobId);
        Task Update(UserProfileEditDTO entity, UserDTO userDTO);
        Task<bool> ResetPasswordAsync(UserDTO user, string token, string newPassword);
        Task<string> GeneratePasswordResetTokenAsync(UserDTO model);

    }
}
