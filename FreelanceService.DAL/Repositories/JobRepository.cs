
using FreelanceService.DAL.Entities;
using FreelanceService.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace FreelanceService.DAL.Repositories
{
    /// <summary>
    /// Job Repository. Implemented CRUD operations in the Users table
    /// </summary>
    public class JobRepository : IJobRepository
    {
        protected readonly IDbContext _context;
        /// <summary>
        /// Dependency Injection
        /// </summary>
        /// <param name="context"></param>
        public JobRepository(IDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Send query for adding a new entry to the table Jobs
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>void</returns>
        public async Task AddJob(Job entity)
        {
            string query = "INSERT INTO Jobs(UserId_Customer,UserId_Executor,CategoryId,Name,Description,City,FinishedDateTime,Price) VALUES(@UserId_Customer,@UserId_Executor,@CategoryId,@Name,@Description,@City,@FinishedDateTime,@Price)";
            await _context.ExecuteAsync(
                 query, param: entity);
        }

        public async Task AddExecutorForJob(int userExecutorId, int jobId)
        {
            string query = "UPDATE Jobs SET UserId_Executor=@UserId_Executor WHERE Id=@Id";

            await _context.ExecuteAsync(
                 query, param: new
                 {
                     UserId_Executor = userExecutorId,
                     Id = jobId
                 });
        }

        /// <summary>
        /// Send query to serch fields in table Jobs equal to id and returns value
        /// </summary>
        /// <param name="id"> id is of type int</param>
        /// <returns>Returns a job  with type Job</returns>
        public async Task<Job> FindJobById(int id)
        {
            string query = "SELECT * FROM Jobs WHERE Id = @id";
            return await _context.QueryFirst<Job>(
                query,
                param: new { Id = id });
        }


        public async Task<Job> FindByIdCustomer(int id)
        {
            string query = "SELECT * FROM Jobs WHERE UserId_Customer = @id";

            return await _context.QueryFirst<Job>(
                query,
                param: new { UserId_Customer = id });
        }


        /// <summary>
        /// Send query to search all entries in the Jobs table
        /// </summary>
        /// <returns> Returns all entries ​​in IEnumerable Jobs type </returns>
        public async Task<IEnumerable<Job>> GetAll()
        {
            string query = "SELECT * FROM Jobs";
            return await _context.Query<Job>(query);
        }

        /// <summary>
        /// Send query to search all entries in the Jobs table
        /// </summary>
        /// <returns> Returns all entries of user ​​in IEnumerable Jobs type </returns>
        public async Task<IEnumerable<Job>> GetAllJobsOfCustomer(int userId)
        {
            string query = "SELECT * FROM Jobs WHERE UserId_Customer=@Id";
            return await _context.Query<Job>(query, param: new { Id = userId });
        }

        /// <summary>
        ///Send query to delete the Jobs table field equal to Id
        /// </summary>
        /// <param name="id">id is of type int</param>
        /// <returns>void</returns>
        public async Task Remove(int id)
        {
            string query = "DELETE FROM Jobs WHERE Id = @id";
            await _context.ExecuteAsync(query, param: new { Id = id });

        }
        /// <summary>
        /// Send quey to update the values ​​of the fields of the Jobs table from the entity parameters
        /// </summary>
        /// <param name="entity">Accepts an entity of type Job</param>
        /// <returns>void</returns>
        public async Task Update(Job entity)
        {
            string query = "UPDATE Jobs SET CategoryId=@CategoryId,Name=@Name,Description=@Description,City=@City,FinishedDateTime=@FinishedDateTime,Price=@Price WHERE Id = @Id";
            await _context.ExecuteAsync(query,
                     param: entity);
        }

        public async Task SelectExectutorForJob(Job entity)
        {
            string query = "UPDATE Jobs SET Status=@Status,UserId_Executor = @UserId_Executor WHERE Id = @Id";
            await _context.ExecuteAsync(query,
                     param: entity);
        }

        public async Task UpdateStatusJob(int jobId, int statusCode)
        {
            string query = "UPDATE Jobs SET Status=@Status WHERE Id = @Id";
            await _context.ExecuteAsync(query,
                     param: new
                     {
                         Id = jobId,
                         Status = statusCode
                     });
        }

        public async Task<IEnumerable<Job>> OrderByAscending(string sortOrder)
        {
            switch (sortOrder)
            {
                case "Price":
                    string queryPrice = "SELECT * FROM Jobs ORDER BY Price ";
                    return await _context.Query<Job>(queryPrice);
                default:
                    string queryName = "SELECT * FROM Jobs ORDER BY Name ";
                    return await _context.Query<Job>(queryName);
            }
        }
        public async Task<IEnumerable<Job>> OrderByDescending(string sortOrder)
        {
            switch (sortOrder)
            {
                case "Price_desc":
                    string query = "SELECT * FROM Jobs ORDER BY Price DESC ";
                    return await _context.Query<Job>(query);
                default:
                    string queryName = "SELECT * FROM Jobs ORDER BY Name DESC ";
                    return await _context.Query<Job>(queryName);
            }
        }


    }
}