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
    public class ReviewRepository : IReviewRepository
    {
        protected readonly IDbConnection _connection;
        protected readonly IDbTransaction _transaction;
        public ReviewRepository(UnitOfWork unitOfWork)
        {
            _connection = unitOfWork.Transaction.Connection;
            _transaction = unitOfWork.Transaction;
        }


        public void AddReview(Review entity)
        {
            string query = "INSERT INTO Reviews VALUES(@Id, @UserId, @Name, @Description, @DateOfWriting, @Feedback, @Rating); SELECT CAST(SCOPE_IDENTITY() as int)";

            if (entity == null)
                throw new ArgumentNullException("entity");


            _connection.Execute(
                query, param: entity, transaction: _transaction
            );

        }

        public Review Find(int id)
        {
            string query = "SELECT * FROM Reviews WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("id");

            return _connection.Query<Review>(
                query,
                param: new { Id = id },
                    transaction: _transaction
            ).FirstOrDefault();
        }

        public IEnumerable<Review> GetAll()
        {
            string query = "SELECT * FROM Reviews";
            return _connection.Query<Review>(query, transaction: _transaction);
        }

        public void Remove(int id)
        {
            string query = "DELETE FROM Reviews WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("entity");
            _connection.Execute(query, transaction: _transaction);

        }

        public void Update(Review entity)
        {
            string query = "UPDATE Reviews SET Id=@Id, UserId=@UserId, Name=@Name, Description=@Description, DateOfWriting=@DateOfWriting, Feedback=@Feedback, Rating=@Rating WHERE Id = @Id";
            _connection.Execute(query,
                    param: entity,
                    transaction: _transaction
                );
        }
    }
}