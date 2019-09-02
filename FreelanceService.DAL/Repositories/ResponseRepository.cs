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
    public class ResponseRepository : IResponseRepository
    {
        protected readonly IDbConnection _connection;
        protected readonly IDbTransaction _transaction;
        public ResponseRepository(UnitOfWork unitOfWork)
        {
            _connection = unitOfWork.Transaction.Connection;
            _transaction = unitOfWork.Transaction;
        }


        public void AddResponse(Response entity)
        {
            string query = "INSERT INTO Responses VALUES(@Id,@UserId_Executor,@TaskId,@Status,@Description,@DateTimeOfResponse);SELECT CAST(SCOPE_IDENTITY() as int)";

            if (entity == null)
                throw new ArgumentNullException("entity");


            _connection.Execute(
                query, param: entity, transaction: _transaction
            );

        }

        public Response Find(int id)
        {
            string query = "SELECT * FROM Responses WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("id");

            return _connection.Query<Response>(
                query,
                param: new { Id = id },
                    transaction: _transaction
            ).FirstOrDefault();
        }

        public IEnumerable<Response> GetAll()
        {
            string query = "SELECT * FROM Responses";
            return _connection.Query<Response>(query, transaction: _transaction);
        }

        public void Remove(int id)
        {
            string query = "DELETE FROM Responses WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("entity");
            _connection.Execute(query, transaction: _transaction);

        }

        public void Update(Response entity)
        {
            string query = "UPDATE Responses SET Id=@Id,UserId_Executor=@UserId_Executor,TaskId=@TaskId,Status=@Status,Description=@Description,DateTimeOfResponse=@DateTimeOfResponse WHERE Id = @Id";
            _connection.Execute(query,
                    param: entity,
                    transaction: _transaction
                );
        }
    }
}