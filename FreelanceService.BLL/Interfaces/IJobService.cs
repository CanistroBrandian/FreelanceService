using FreelanceService.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreelanceService.BLL.Interfaces
{
    public interface IJobService
    {
        Task AddJob(JobDTO entity, UserDTO user);
        Task<JobDTO> FindJobById(int id);
        Task<IEnumerable<JobDTO>> GetAll();
        Task<IEnumerable<JobDTO>> GetAllJobsOfCustomer(UserDTO user);
        Task Update(JobDTO entity);
        Task<IEnumerable<JobDTO>> GetAllSorting(string orderSort);
        IEnumerable<JobDTO> Search(string searchString, IEnumerable<JobDTO> list);
        Task CommitAsync();
    }
}
