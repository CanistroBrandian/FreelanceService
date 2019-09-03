using Dapper;
using FreelanceService.DAL.Entities;
using FreelanceService.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreelanceService.DAL.Repositories
{
    public class ResponseRepository : IResponseRepository
    {
        protected readonly IDbContext _context;
        public ResponseRepository(IDbContext context)
        {
            _context = context;
        }


        public void AddResponse(Response entity)
        {
            string query = "INSERT INTO Responses VALUES(@Id,@UserId_Executor,@TaskId,@Status,@Description,@DateTimeOfResponse);SELECT CAST(SCOPE_IDENTITY() as int)";

            if (entity == null)
                throw new ArgumentNullException("entity");


            _context.Execute(
                query, param: entity
            );

        }

        public Response Find(int id)
        {
            string query = "SELECT * FROM Responses WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("id");

            return _context.Query<Response>(
                query,
                param: new { Id = id }).FirstOrDefault();
        }

        public IEnumerable<Response> GetAll()
        {
            string query = "SELECT * FROM Responses";
            return _context.Query<Response>(query);
        }

        public void Remove(int id)
        {
            string query = "DELETE FROM Responses WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("entity");
            _context.Execute(query);

        }

        public void Update(Response entity)
        {
            string query = "UPDATE Responses SET Id=@Id,UserId_Executor=@UserId_Executor,TaskId=@TaskId,Status=@Status,Description=@Description,DateTimeOfResponse=@DateTimeOfResponse WHERE Id = @Id";
            _context.Execute(query,
                    param: entity);
        }
    }
}