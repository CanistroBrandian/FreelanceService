using Dapper;
using FreelanceService.DAL.Entities;
using FreelanceService.DAL.Interfaces;
using System;
using System.Collections.Generic;
using FreelanceService.Common.Enum;
using System.Threading.Tasks;


namespace FreelanceService.DAL.Repositories
{
    public class ResponseRepository : IResponseRepository
    {
        protected readonly IDbContext _context;
        public ResponseRepository(IDbContext context)
        {
            _context = context;
        }

        public async Task AddResponse(Response response, int userExecutorId, int jobId)
        {
            string query = "if((SELECT COUNT(*) AS result FROM Responses  WHERE JobId = @JobId and UserId_Executor = @UserId_Executor) = 0) INSERT INTO Responses(UserId_Executor, JobId, Status, Description, Price) VALUES(@UserId_executor, @JobId, @Status, @Description, @Price)";
            await _context.ExecuteAsync(query, param: new {
                UserId_Executor = userExecutorId,
                JobId = jobId,
                Description = response.Description,
                Status = 1,
                Price = response.Price
                }
                );

        }

        public async Task<Response> FindResponseById(int id)
        {
            string query = "SELECT * FROM Responses WHERE Id = @id";

            return await _context.QueryFirst<Response>(
                query,
                param: new { Id = id });
        }

        public async Task<Response> FindResponseByJobId(int jobId)
        {
            string query = "SELECT * FROM Responses WHERE JobId = @Id";
            return await _context.QueryFirst<Response>(
                query,
                param: new { Id = jobId });
        }

        public async Task<IEnumerable<Response>> FindResponseByUserExecutorId(int userId_executor)
        {
            string query = "SELECT * FROM Responses WHERE UserId_Executor = @Id";
            return await _context.Query<Response>(
                query,
                param: new { Id = userId_executor });
        }

       public async Task<Response> FindResponseByIdExecutorAndJobId(int userId_executor, int jobId)
        {
            string query = "SELECT * FROM Responses WHERE UserId_Executor = @UserId_Executor AND JobId=@JobId";

            return await _context.QueryFirst<Response>(
                query,
                param: new
                {
                    UserId_Executor = userId_executor,
                    JobId = jobId
                });
        }

        public async Task<IEnumerable<Response>> GetAll()
        {
            string query = "SELECT * FROM Responses";
            return await _context.Query<Response>(query);
        }

        public async Task<IEnumerable<Response>> GetAllResponseOfJob(int id)
        {
            string query = "SELECT * FROM Responses WHERE JobId=@Id";
            return await _context.Query<Response>(query,param: new { Id = id });
        }

        public async Task Remove(int id)
        {
            string query = "DELETE FROM Responses WHERE Id = @id";
            await _context.ExecuteAsync(query, param: new {Id = id});

        }

        public async Task Update(Response entity)
        {
            string query = "UPDATE Responses SET JobId = @JobId, Description = @Description, Price = @Price WHERE Id = @Id";
            await _context.ExecuteAsync(query,
                     param: entity);
        }

        
    }
}