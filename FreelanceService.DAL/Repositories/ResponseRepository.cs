using Dapper;
using FreelanceService.DAL.Entities;
using FreelanceService.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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


        public async Task AddResponse(Response entity)
        {
            string query = "INSERT INTO Responses VALUES(@Id,@UserId_Executor,@TaskId,@Status,@Description,@DateTimeOfResponse);SELECT CAST(SCOPE_IDENTITY() as int)";

            if (entity == null)
                throw new ArgumentNullException("entity");

            await _context.ExecuteAsync(query, param: entity);

        }

        public async Task<Response> FindById(int id)
        {
            string query = "SELECT * FROM Responses WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("id");

            return await _context.QueryFirst<Response>(
                query,
                param: new { Id = id });
        }

        public async Task<IEnumerable<Response>> GetAll()
        {
            string query = "SELECT * FROM Responses";
            return await _context.Query<Response>(query);
        }

        public async Task Remove(int id)
        {
            string query = "DELETE FROM Responses WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("entity");
            await _context.ExecuteAsync(query);

        }

        public async Task Update(Response entity)
        {
            string query = "UPDATE Responses SET Id=@Id,UserId_Executor=@UserId_Executor,TaskId=@TaskId,Status=@Status,Description=@Description,DateTimeOfResponse=@DateTimeOfResponse WHERE Id = @Id";
            await _context.ExecuteAsync(query,
                     param: entity);
        }
    }
}