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
        private readonly IUserService _userService;
        private readonly IResponseService _responseService;
        public JobService(IUnitOfWork uow, IMapper mapper, IUserService userService, IResponseService responseService)
        {
            _uow = uow;
            _mapper = mapper;
            _userService = userService;
            _responseService = responseService;
        }

        public async Task AddJob(JobDTO model, UserDTO user)
        {
            var job = _mapper.Map<JobDTO, Job>(model);
            job.UserId_Customer = user.Id;
            await _uow.JobRepos.AddJob(job);
            await CommitAsync();
        }

        public async Task<JobDTO> FindJobById(int id)
        {

            var entity = await _uow.JobRepos.FindJobById(id);
            return _mapper.Map<Job, JobDTO>(entity);
        }

        public async Task<JobDTO> FindJobByIdCustomer(int id)
        {
            var entity = await _uow.JobRepos.FindByIdCustomer(id);
            return _mapper.Map<Job, JobDTO>(entity);
        }

        public async Task<IEnumerable<JobDTO>> GetAllJobsOfCustomer(UserDTO userDTO)
        {
            var user = _mapper.Map<UserDTO, User>(userDTO);
            
            var map = _mapper.Map<IEnumerable<Job>, IEnumerable<JobDTO>>(await _uow.JobRepos.GetAllJobsOfCustomer(user));
            await CommitAsync();
            return map;
        }

        public  async Task<(JobDTO , UserDTO ,IEnumerable<ResponseDTO> , IEnumerable<UserDTO> )>  JobDetails(int jobId)
        {
            var jobDTO = await FindJobById(jobId);
            var userCustomer = await _userService.FindUserById(jobDTO.UserId_Customer);
            var allResponsesOfJob = await _responseService.GetAllResponseOfJob(jobDTO.Id);
            var getAllUsersExecutorsOfResponse = await _userService.GetAllUsersExecutorsOfResponse(jobDTO.Id);
            var result=(jobDTO, userCustomer, allResponsesOfJob, getAllUsersExecutorsOfResponse);
            return result;
        }

        public async Task Update(JobDTO entity)
        {
            var job = _mapper.Map<JobDTO, Job>(entity);
            await _uow.JobRepos.Update(job);
            await CommitAsync();
        }

        public async Task Remove(JobDTO entity)
        {
            await _uow.JobRepos.Remove(entity.Id);
            await CommitAsync();
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


        private async Task CommitAsync()
        {
            await _uow.CommitAsync();
        }


    }
}
