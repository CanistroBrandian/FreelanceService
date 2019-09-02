using Dapper;
using FreelanceService.DAL.Concrate;
using FreelanceService.DAL.Entities;
using FreelanceService.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FreelanceService.DAL.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        protected readonly IDbConnection _connection;
        protected readonly IDbTransaction _transaction;
        public TaskRepository(UnitOfWork unitOfWork)
        {
            _connection = unitOfWork.Transaction.Connection;
            _transaction = unitOfWork.Transaction;
        }


        public void AddTask(Task entity)
        {
            string query = "INSERT INTO Tasks VALUES(@Id,@UserId_Executor,@CategoryId,@Name,@Description,@City,@Status,@StartDate,@FinishedDate,@Price);SELECT CAST(SCOPE_IDENTITY() as int)";

            if (entity == null)
                throw new ArgumentNullException("entity");


            _connection.Execute(
                query, param: entity, transaction: _transaction
            );

        }

        public Task Find(int id)
        {
            string query = "SELECT * FROM Tasks WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("id");

            return _connection.Query<Task>(
                query,
                param: new { Id = id },
                    transaction: _transaction
            ).FirstOrDefault();
        }

        public IEnumerable<Task> GetAll()
        {
            string query = "SELECT * FROM Tasks";
            return _connection.Query<Task>(query, transaction: _transaction);
        }

        public void Remove(int id)
        {
            string query = "DELETE FROM Tasks WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("entity");
            _connection.Execute(query, transaction: _transaction);

        }

        public void Update(Task entity)
        {
            string query = "UPDATE Tasks SET Id=@Id,UserId_Executor=@UserId_Executor,CategoryId=@CategoryId,Name=@Name,Description=@Description,City=@City,Status=@Status,StartDate=@StartDate,FinishedDate=@FinishedDate,Price=@Price WHERE Id = @Id";
            _connection.Execute(query,
                    param: entity,
                    transaction: _transaction
                );
        }
    }
}