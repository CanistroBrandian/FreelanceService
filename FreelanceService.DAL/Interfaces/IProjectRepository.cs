using FreelanceService.DAL.Entities;
using System.Collections.Generic;

namespace FreelanceService.DAL.Interfaces
{
    public interface IProjectRepository
    {
        void AddProject(Project entity);
        IEnumerable<Project> GetAll();
        Project Find(int id);
        void Remove(int id);
        void Update(Project entity);
    }
}
