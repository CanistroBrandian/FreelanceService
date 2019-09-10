
using FreelanceService.DAL.Entities;
using FreelanceService.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace FreelanceService.DAL.Repositories
{
    public class JobRepository : IJobRepository
    {
        protected readonly IDbContext _context;
        public JobRepository(IDbContext context)
        {
            _context = context;
        }


        public async Task AddJob(Job entity)
        {
            string query = "INSERT INTO Jobs VALUES(@Id,@UserId_Executor,@CategoryId,@Name,@Description,@City,@Status,@RegistrationJobDateTime,@StartDate,@FinishedDate,@Price);SELECT CAST(SCOPE_IDENTITY() as int)";

            if (entity == null)
                throw new ArgumentNullException("entity");


           await _context.ExecuteAsync(
                query, param: entity);

        }

 

        public async Task<Job> FindById(int id)
        {
            string query = "SELECT * FROM Jobs WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("id");

            return await _context.QueryFirst<Job>(
                query,
                param: new { Id = id });
        }

        public async Task<IEnumerable<Job>> GetAll()
        {
            string query = "SELECT * FROM Jobs";
            return await _context.Query<Job>(query);
        }

        public async Task Remove(int id)
        {
            string query = "DELETE FROM Jobs WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("entity");
           await _context.ExecuteAsync(query);

        }

        public async Task Update(Job entity)
        {
            string query = "UPDATE Jobs SET Id=@Id,UserId_Executor=@UserId_Executor,CategoryId=@CategoryId,Name=@Name,Description=@Description,City=@City,Status=@Status,StartDate=@StartDate,FinishedDate=@FinishedDate,Price=@Price WHERE Id = @Id";
           await _context.ExecuteAsync(query,
                    param: entity);
        }

    }
}