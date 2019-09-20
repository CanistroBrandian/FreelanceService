using FreelanceService.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceService.BLL.Interfaces
{
   public interface IProjectService
    {
        Task AddProject(ProjectDTO entity);
        Task<ProjectDTO> FindProjectById(int id);
        Task<IEnumerable<ProjectDTO>> GetAll();
        Task Update(ProjectDTO entity);
        Task CommitAsync();
    }
}
