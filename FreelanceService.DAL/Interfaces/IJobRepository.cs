using FreelanceService.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreelanceService.DAL.Interfaces
{
    public interface IJobRepository
    {
        Task AddJob(Job entity);
        Task<IEnumerable<Job>> GetAll();
        Task<Job> FindById(int id);
        Task Remove(int id);
        Task Update(Job entity);
    }
}
