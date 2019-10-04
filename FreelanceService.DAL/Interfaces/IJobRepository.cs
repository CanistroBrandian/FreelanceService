﻿using FreelanceService.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreelanceService.DAL.Interfaces
{
    public interface IJobRepository
    {
        Task AddJob(Job entity);
        Task AddExecutorForJob(int userExecutorId, int jobid);
        Task<IEnumerable<Job>> GetAll();
        Task<IEnumerable<Job>> GetAllJobsOfCustomer(User entity);

        Task<Job> FindJobById(int id);
        Task<Job> FindByIdCustomer(int id);
        Task Remove(int id);
        Task Update(Job entity);
        Task<IEnumerable<Job>> OrderByDescending(string sortOrder);
        Task<IEnumerable<Job>> OrderByAscending(string sortOrder);
    }
}
