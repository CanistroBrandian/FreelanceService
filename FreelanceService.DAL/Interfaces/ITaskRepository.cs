using FreelanceService.DAL.Entities;
using System.Collections.Generic;

namespace FreelanceService.DAL.Interfaces
{
    public interface ITaskRepository
    {
        void AddTask(Task entity);
        IEnumerable<Task> GetAll();
        Task Find(int id);
        void Remove(int id);
        void Update(Task entity);
    }
}
