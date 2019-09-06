using FreelanceService.DAL.Entities;
using System.Collections.Generic;

namespace FreelanceService.DAL.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(User entity);
        IEnumerable<User> GetAll();
        User FindById(int id);
        User FindByEmail(string email);
        void Remove(int id);
        void Update(User entity);
    }
}
