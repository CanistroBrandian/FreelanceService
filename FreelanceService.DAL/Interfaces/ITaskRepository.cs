using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreelanceService.DAL.Interfaces
{
    public interface ITaskRepository
    {
        Task AddTask(FreelanceService.DAL.Entities.Task entity);
        Task<IEnumerable<FreelanceService.DAL.Entities.Task>> GetAll();
        Task FindById(int id);
        Task Remove(int id);
        Task Update(FreelanceService.DAL.Entities.Task entity);
    }
}
