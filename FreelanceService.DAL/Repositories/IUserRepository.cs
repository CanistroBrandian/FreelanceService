using FreelanceService.DAL.Entities;
using System.Collections.Generic;

namespace FreelanceService.DAL.Repositories
{
    public interface IUserRepository
    {
        void AddUser(User entity);
        IEnumerable<User> GetAll();
        User Find(int id);
        void Remove(int id);
        void Update(User entity);
    }
}
