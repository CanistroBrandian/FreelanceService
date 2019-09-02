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
    public class CategoryRepository:ICategoryRepository
    {
        protected readonly IDbConnection _connection;
        protected readonly IDbTransaction _transaction;
        public CategoryRepository(UnitOfWork unitOfWork)
        {
            _connection = unitOfWork.Transaction.Connection;
            _transaction = unitOfWork.Transaction;
        }


        public void AddCategory(Category entity)
        {
            string query = "INSERT INTO Categories VALUES(@Id,@Name);SELECT CAST(SCOPE_IDENTITY() as int)";

            if (entity == null)
                throw new ArgumentNullException("entity");


            _connection.Execute(
                query, param: entity, transaction: _transaction
            );

        }

        public Category Find(int id)
        {
            string query = "SELECT * FROM Categories WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("id");

            return _connection.Query<Category>(
                query,
                param: new { Id = id },
                    transaction: _transaction
            ).FirstOrDefault();
        }

        public IEnumerable<Category> GetAll()
        {
            string query = "SELECT * FROM Categories";
            return _connection.Query<Category>(query, transaction: _transaction);
        }

        public void Remove(int id)
        {
            string query = "DELETE FROM Categories WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("entity");
            _connection.Execute(query, transaction: _transaction);

        }

        public void Update(Category entity)
        {
            string query = "UPDATE Categorys SET Id=@Id, Name=@Name WHERE Id = @Id";
            _connection.Execute(query,
                    param: entity,
                    transaction: _transaction
                );
        }
    }
}

