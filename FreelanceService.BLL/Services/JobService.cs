using AutoMapper;
using FreelanceService.BLL.DTO;
using FreelanceService.BLL.Interfaces;
using FreelanceService.DAL.Entities;
using FreelanceService.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceService.BLL.Services
{
    public class JobService : IJobService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public JobService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task AddJob(JobDTO model, UserDTO user)
        {

            var job = _mapper.Map<JobDTO, Job>(model);
            job.UserId_Customer = user.Id;

        }

        public async Task<JobDTO> FindJobById(int id)
        {
            if (id == 0)
                throw new Exception("Поле Id не введено");
            var entity = await _uow.JobRepos.FindById(id);
            return _mapper.Map<Job, JobDTO>(entity);
        }

        public async Task<IEnumerable<JobDTO>> GetAllJobsOfCustomer(UserDTO userDTO)
        {
            var user = _mapper.Map<UserDTO, User>(userDTO);
            var map = _mapper.Map<IEnumerable<Job>, IEnumerable<JobDTO>>(await _uow.JobRepos.GetAllJobsOfCustomer(user));
            return map;
        }

        public async Task<IEnumerable<JobDTO>> GetAll()
        {
            var users = await _uow.JobRepos.GetAll();
            var result = _mapper.Map<IEnumerable<Job>, IEnumerable<JobDTO>>(users);
            return result;
        }

        public async Task Update(JobDTO entity)
        {
            var job = _mapper.Map<JobDTO, Job>(entity);
            await _uow.JobRepos.Update(job);
        }

        public async Task Remove(JobDTO entity)
        {
            await _uow.JobRepos.Remove(entity.Id);
        }

        public async Task<IEnumerable<JobDTO>> GetAllSorting(string sortOrder)
        {
            switch (sortOrder)
            {
                case "Name_desc":
                    var jobNamesOrderDes = await _uow.JobRepos.OrderByDescending(sortOrder);
                    ;
                    await CommitAsync();
                    return _mapper.Map<IEnumerable<Job>, IEnumerable<JobDTO>>(jobNamesOrderDes);
                case "Price":
                    var jobPricesOrderAsc = await _uow.JobRepos.OrderByAscending(sortOrder);
                    await CommitAsync();
                    return _mapper.Map<IEnumerable<Job>, IEnumerable<JobDTO>>(jobPricesOrderAsc);
                case "Price_desc":
                    var jobPricesOrderDes = await _uow.JobRepos.OrderByDescending(sortOrder);
                    await CommitAsync();
                    return _mapper.Map<IEnumerable<Job>, IEnumerable<JobDTO>>(jobPricesOrderDes);
                default:
                    var jobNameOrderAsc = await _uow.JobRepos.OrderByAscending(sortOrder);
                    await CommitAsync();
                    return _mapper.Map<IEnumerable<Job>, IEnumerable<JobDTO>>(jobNameOrderAsc);

            }
        }

        public IEnumerable<JobDTO> Search(string searchString, IEnumerable<JobDTO> list)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                return list.Where(s => s.Name.Contains(searchString));
            }
            else return list;
        }


        public async Task CommitAsync()
        {
            await _uow.CommitAsync();
        }
    }
}
