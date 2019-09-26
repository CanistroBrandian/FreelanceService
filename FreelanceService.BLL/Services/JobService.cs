using AutoMapper;
using FreelanceService.BLL.DTO;
using FreelanceService.BLL.Interfaces;
using FreelanceService.BLL.Models;
using FreelanceService.Common.Enum;
using FreelanceService.DAL.Entities;
using FreelanceService.DAL.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
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

        public async Task AddJob(CreateJobViewModel createModel, UserDTO user)
        {
            var model = _mapper.Map<CreateJobViewModel, JobDTO>(createModel);
            var job = _mapper.Map<JobDTO, Job>(model);
            job.UserId_Customer = user.Id;
            if (user.Role == (int)RoleEnum.Customer)
                await _uow.JobRepos.AddJob(job);
            else throw new ArgumentException("Невозможно добавить новую задачу т.к. пользователь не является заказчиком");

        }

        public async Task<JobDTO> FindJobById(int id)
        {
            if (id == 0)
                throw new Exception("Поле Id не введено");
            var entity = await _uow.JobRepos.FindById(id);
            return _mapper.Map<Job, JobDTO>(entity);
        }

        public async Task<IEnumerable<JobViewDTO>> GetAllJobsOfCustomer(UserDTO userDTO)
        {
            if (userDTO.Role == (int)RoleEnum.Customer && userDTO.Id != 0)
            {
                var user = _mapper.Map<UserDTO, User>(userDTO);
                var result = _mapper.Map<IEnumerable<Job>, IEnumerable<JobViewDTO>>(await _uow.JobRepos.GetAllJobsOfCustomer(user));
                return result;
            }
            else throw new Exception("Роль или Такого пользователя не существует");

        }

        public async Task<IEnumerable<JobViewDTO>> GetAll()
        {
            var users = await _uow.JobRepos.GetAll();
            var result = _mapper.Map<IEnumerable<Job>, IEnumerable<JobViewDTO>>(users);
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

        public async Task<IEnumerable<JobViewDTO>> GetAllSorting(string sortOrder)
        {
            switch (sortOrder)
            {
                case "Name_desc":
                    var jobNamesOrderDes = await _uow.JobRepos.OrderByDescending(sortOrder);
                    ;
                    await CommitAsync();
                    return _mapper.Map<IEnumerable<Job>, IEnumerable<JobViewDTO>>(jobNamesOrderDes);
                case "Price":
                    var jobPricesOrderAsc = await _uow.JobRepos.OrderByAscending(sortOrder);
                    await CommitAsync();
                    return _mapper.Map<IEnumerable<Job>, IEnumerable<JobViewDTO>>(jobPricesOrderAsc);
                case "Price_desc":
                    var jobPricesOrderDes = await _uow.JobRepos.OrderByDescending(sortOrder);
                    await CommitAsync();
                    return _mapper.Map<IEnumerable<Job>, IEnumerable<JobViewDTO>>(jobPricesOrderDes);
                default:
                    var jobNameOrderAsc = await _uow.JobRepos.OrderByAscending(sortOrder);
                    await CommitAsync();
                    return _mapper.Map<IEnumerable<Job>, IEnumerable<JobViewDTO>>(jobNameOrderAsc);

            }
        }

        public  IEnumerable<JobViewDTO> Search(string searchString, IEnumerable<JobViewDTO> list)
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
