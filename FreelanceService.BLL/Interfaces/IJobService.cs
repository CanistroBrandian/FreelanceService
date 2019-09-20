using FreelanceService.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceService.BLL.Interfaces
{
   public interface IJobService
    {
        Task AddJob(JobDTO entity);
        Task<JobDTO> FindJobById(int id);
        Task<IEnumerable<JobDTO>> GetAll();
        Task Update(JobDTO entity);
        Task CommitAsync();
    }
}
