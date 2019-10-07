using FreelanceService.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreelanceService.BLL.Interfaces
{
    public interface IJobService
    {
        Task AddJob(JobDTO entity, UserDTO user);
        Task<JobDTO> FindJobById(int id);
        Task<JobDTO> FindJobByIdCustomer(int id);
        Task<IEnumerable<JobDTO>> GetAllJobsOfCustomer(int userId);
        Task SelectExecutorForJob(int jobId, int userId_Executor);
        Task Update(int jobId, JobDTO entity);
        Task Remove(int jobId);
        Task<IEnumerable<JobDTO>> GetAllSorting(string orderSort);
        IEnumerable<JobDTO> Search(string searchString, IEnumerable<JobDTO> list);
    }
}
