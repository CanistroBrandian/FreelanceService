using AutoMapper;
using FreelanceService.BLL.DTO;
using FreelanceService.BLL.Interfaces;
using FreelanceService.DAL.Entities;
using FreelanceService.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceService.BLL.Services
{
   public class ProjectService:IProjectService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ProjectService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task AddProject(ProjectDTO entity)
        {
            var project = _mapper.Map<ProjectDTO, Project>(entity);
            await _uow.ProjectRepos.AddProject(project);

        }


        public async Task<ProjectDTO> FindProjectById(int id)
        {
            if (id == 0)
                throw new Exception("Поле Id не введено");
            var entity = await _uow.ProjectRepos.FindById(id);
            return _mapper.Map<Project, ProjectDTO>(entity);
        }

        public async Task<IEnumerable<ProjectDTO>> GetAll()
        {
            var result = _mapper.Map<IEnumerable<Project>, IEnumerable<ProjectDTO>>(await _uow.ProjectRepos.GetAll());
            return result;
        }

        public async Task Update(ProjectDTO entity)
        {

            var project = _mapper.Map<ProjectDTO, Project>(entity);
            await _uow.ProjectRepos.Update(project);
        }

        public async Task Remove(ResponseDTO entity)
        {
            await _uow.ProjectRepos.Remove(entity.Id);
        }

        public async Task CommitAsync()
        {
            await _uow.CommitAsync();
        }
    }
}
