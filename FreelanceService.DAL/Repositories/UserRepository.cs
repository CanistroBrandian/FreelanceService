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
    public class UserRepository : IUserRepository
    {
        protected readonly IDbConnection _connection;
        protected readonly IDbTransaction _transaction;
        public UserRepository(UnitOfWork unitOfWork)
        {
            _connection = unitOfWork.Transaction.Connection;
            _transaction = unitOfWork.Transaction;
        }
     

        public void AddUser(User entity)
        {
            string query = "INSERT INTO Users(Id,Email,PassHash,FirstName,LastName,Phone,DynamicSalt,DateRegistration,Image,Rating,Role)" +
                "VALUES(@Id,@Email,@PassHash,@FirstName,@LastName,@Phone,@DynamicSalt,@DateRegistration,@Image,@Rating,@Role);SELECT CAST(SCOPE_IDENTITY() as int)";

            if (entity == null)
                throw new ArgumentNullException("entity");


            _connection.Execute(
                query, param:new
                {
                    Id = entity.Id,
                    Email = entity.Email,
                    PassHash= entity.PassHash,
                    FirstName= entity.FirstName,
                    LastName= entity.LastName,
                    Phone= entity.Phone,
                    DynamicSalt= entity.DynamicSalt,
                    DateRegistration = entity.DateRegistration,
                    Image = entity.Image,
                    Rating = entity.Rating,
                    Role = entity.Role

                }
                , transaction: _transaction
            );

        }

        public User Find(int id)
        {
            string query = "SELECT * FROM Users WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("id");

            return _connection.Query<User>(
                query,
                param: new { Id = id },
                    transaction: _transaction
            ).FirstOrDefault();
        }

        public IEnumerable<User> GetAll()
        {
            string query = "SELECT * FROM Users";
            return _connection.Query<User>(query, transaction: _transaction);
        }

        public void Remove(int id)
        {
            string query = "DELETE FROM Users WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("entity");
            _connection.Execute(query, transaction: _transaction);

        }

        public void Update(User entity)
        {
            string query = "UPDATE Users SET {0}=@{0},{1}=@{1},{2}=@{2},{3}=@{3},{4}=@{4},{5}=@{5},{6}=@{6},{7}=@{7},{8}=@{8},{9}=@{9},{10}=@{10} WHERE Id = @Id",
                 Id, Email, PassHash, FirstName, LastName, Phone, DynamicSalt, DateRegistration, Image, Rating, Role;
            _connection.Execute(query,
                    param: new
                    {
                        Id = entity.Id,
                        Email = entity.Email,
                        PassHash = entity.PassHash,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        Phone = entity.Phone,
                        DynamicSalt = entity.DynamicSalt,
                        DateRegistration = entity.DateRegistration,
                        Image = entity.Image,
                        Rating = entity.Rating,
                        Role = entity.Role
                    },
                    transaction: _transaction
                );
        }
    }
}
