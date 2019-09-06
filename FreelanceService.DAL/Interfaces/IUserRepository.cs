using FreelanceService.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace FreelanceService.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task AddUser(User entity);
        Task<IEnumerable<User>> GetAll();
        Task<User> FindById(int id);
        Task<User> FindByEmail(string email);
        Task Remove(int id);
        Task Update(User entity);
    }
}
