using FreelanceService.BLL.DTO;
using FreelanceService.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreelanceService.BLL.Interfaces
{
    public interface IJobService
    {
        Task AddJob(CreateJobViewModel entity, UserDTO user);
        Task<JobDTO> FindJobById(int id);
        Task<IEnumerable<JobViewDTO>> GetAll();
        Task<IEnumerable<JobViewDTO>> GetAllJobsOfCustomer(UserDTO user);
        Task Update(JobDTO entity);
        Task CommitAsync();
    }
}
