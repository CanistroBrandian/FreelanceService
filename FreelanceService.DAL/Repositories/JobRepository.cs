
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
            string query = "INSERT INTO Jobs(UserId_Customer,UserId_Executor,CategoryId,Name,Description,City,Status,FinishedDateTime,Price) VALUES(@UserId_Customer,@UserId_Executor,@CategoryId,@Name,@Description,@City,@Status,@FinishedDateTime,@Price)";
            if (entity == null)
                throw new ArgumentNullException("entity");
           await _context.ExecuteAsync(
                query, param: entity);

        }
        /// <summary>
        /// Send query to serch fields in table Jobs equal to id and returns value
        /// </summary>
        /// <param name="id"> id is of type int</param>
        /// <returns>Returns a job  with type Job</returns>
        public async Task<Job> FindById(int id)
        {
            string query = "SELECT * FROM Jobs WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("id");

            return await _context.QueryFirst<Job>(
                query,
                param: new { Id = id });
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
        public async Task<IEnumerable<Job>> GetAllJobsOfCustomer(User user)
        {
            string query = "SELECT * FROM Jobs WHERE Id=@Id";
            return await _context.Query<Job>(query, param: new {Id= user.Id});
        }

        /// <summary>
        ///Send query to delete the Jobs table field equal to Id
        /// </summary>
        /// <param name="id">id is of type int</param>
        /// <returns>void</returns>
        public async Task Remove(int id)
        {
            string query = "DELETE FROM Jobs WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("entity");
           await _context.ExecuteAsync(query);

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

    }
}