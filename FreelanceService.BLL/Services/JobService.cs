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
   public class JobService:IJobService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public JobService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task AddJob(JobDTO entity)
        {
            var job = _mapper.Map<JobDTO, Job>(entity);
            await _uow.JobRepos.AddJob(job);

        }


        public async Task<JobDTO> FindJobById(int id)
        {
            if (id == 0)
                throw new Exception("Поле Id не введено");
            var entity = await _uow.JobRepos.FindById(id);
            return _mapper.Map<Job, JobDTO>(entity);
        }

        public async Task<IEnumerable<JobDTO>> GetAll()
        {
            var result = _mapper.Map<IEnumerable<Job>, IEnumerable<JobDTO>>(await _uow.JobRepos.GetAll());
            return result;
        }

        public async Task Update(JobDTO entity)
        {
            var job = _mapper.Map<JobDTO, Job>(entity);
            await _uow.JobRepos.Update(job);
        }

        public async Task Remove(ResponseDTO entity)
        {
            await _uow.JobRepos.Remove(entity.Id);
        }

        public async Task CommitAsync()
        {
            await _uow.CommitAsync();
        }
    }
}
