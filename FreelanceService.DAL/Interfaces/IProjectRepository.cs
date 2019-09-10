using FreelanceService.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace FreelanceService.DAL.Interfaces
{
    public interface IProjectRepository
    {
        Task AddProject(Project entity);
        Task<IEnumerable<Project>> GetAll();
        Task<Project> FindById(int id);
        Task Remove(int id);
        Task Update(Project entity);
    }
}
