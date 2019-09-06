using FreelanceService.DAL.Entities;
using FreelanceService.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreelanceService.DAL.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        protected readonly IDbContext _context;
        public TaskRepository(IDbContext context)
        {
            _context = context;
        }


        public void AddTask(Task entity)
        {
            string query = "INSERT INTO Tasks VALUES(@Id,@UserId_Executor,@CategoryId,@Name,@Description,@City,@Status,@RegistrationTaskDateTime,@StartDate,@FinishedDate,@Price);SELECT CAST(SCOPE_IDENTITY() as int)";

            if (entity == null)
                throw new ArgumentNullException("entity");


            _context.Execute(
                query, param: entity);

        }

        public Task Find(int id)
        {
            string query = "SELECT * FROM Tasks WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("id");

            return _context.Query<Task>(
                query,
                param: new { Id = id }).FirstOrDefault();
        }

        public IEnumerable<Task> GetAll()
        {
            string query = "SELECT * FROM Tasks";
            return _context.Query<Task>(query);
        }

        public void Remove(int id)
        {
            string query = "DELETE FROM Tasks WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("entity");
            _context.Execute(query);

        }

        public void Update(Task entity)
        {
            string query = "UPDATE Tasks SET Id=@Id,UserId_Executor=@UserId_Executor,CategoryId=@CategoryId,Name=@Name,Description=@Description,City=@City,Status=@Status,StartDate=@StartDate,FinishedDate=@FinishedDate,Price=@Price WHERE Id = @Id";
            _context.Execute(query,
                    param: entity);
        }
    }
}