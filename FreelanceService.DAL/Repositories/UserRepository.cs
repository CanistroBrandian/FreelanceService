using FreelanceService.DAL.Entities;
using FreelanceService.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreelanceService.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {

        protected readonly IDbContext _context;
        public UserRepository(IDbContext context)
        {
            _context = context;
        }


        public void AddUser(User entity)
        {
            string query = "INSERT INTO Users(Id,Email,PassHash,FirstName,LastName,Phone,DynamicSalt,DateRegistration,Image,Rating,Role)" +
                "VALUES(@Id,@Email,@PassHash,@FirstName,@LastName,@Phone,@DynamicSalt,@DateRegistration,@Image,@Rating,@Role);SELECT CAST(SCOPE_IDENTITY() as int)";

            if (entity == null)
                throw new ArgumentNullException("entity");


            _context.Execute(
                query, param: new
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

                }
            );

        }

        public User Find(int id)
        {
            string query = "SELECT * FROM Users WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("id");

            return _context.Query<User>(
                query,
                param: new { Id = id }
            ).FirstOrDefault();
        }

        public IEnumerable<User> GetAll()
        {
            string query = "SELECT * FROM Users";
            return _context.Query<User>(query);
        }

        public void Remove(int id)
        {
            string query = "DELETE FROM Users WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("entity");
            _context.Execute(query);

        }

        public void Update(User entity)
        {
            string query = "UPDATE Users SET {0}=@{0},{1}=@{1},{2}=@{2},{3}=@{3},{4}=@{4},{5}=@{5},{6}=@{6},{7}=@{7},{8}=@{8},{9}=@{9},{10}=@{10} WHERE Id = @Id",
                 Id, Email, PassHash, FirstName, LastName, Phone, DynamicSalt, DateRegistration, Image, Rating, Role;
            _context.Execute(query,
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
                    }
                );
        }
    }
}
