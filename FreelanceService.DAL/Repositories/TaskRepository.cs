
using FreelanceService.DAL.Entities;
using FreelanceService.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace FreelanceService.DAL.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        protected readonly IDbContext _context;
        public TaskRepository(IDbContext context)
        {
            _context = context;
        }


        public async Task AddTask(FreelanceService.DAL.Entities.Task entity)
        {
            string query = "INSERT INTO Tasks VALUES(@Id,@UserId_Executor,@CategoryId,@Name,@Description,@City,@Status,@RegistrationTaskDateTime,@StartDate,@FinishedDate,@Price);SELECT CAST(SCOPE_IDENTITY() as int)";

            if (entity == null)
                throw new ArgumentNullException("entity");


           await _context.Execute(
                query, param: entity);

        }

 

        public async Task<FreelanceService.DAL.Entities.Task> FindById(int id)
        {
            string query = "SELECT * FROM Tasks WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("id");

            return await _context.QueryFirst<FreelanceService.DAL.Entities.Task>(
                query,
                param: new { Id = id });
        }

        public async Task<IEnumerable<FreelanceService.DAL.Entities.Task>> GetAll()
        {
            string query = "SELECT * FROM Tasks";
            return await _context.Query<FreelanceService.DAL.Entities.Task>(query);
        }

        public async Task Remove(int id)
        {
            string query = "DELETE FROM Tasks WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("entity");
           await _context.Execute(query);

        }

        public async Task Update(FreelanceService.DAL.Entities.Task entity)
        {
            string query = "UPDATE Tasks SET Id=@Id,UserId_Executor=@UserId_Executor,CategoryId=@CategoryId,Name=@Name,Description=@Description,City=@City,Status=@Status,StartDate=@StartDate,FinishedDate=@FinishedDate,Price=@Price WHERE Id = @Id";
           await _context.Execute(query,
                    param: entity);
        }

        System.Threading.Tasks.Task ITaskRepository.FindById(int id)
        {
            throw new NotImplementedException();
        }
    }
}