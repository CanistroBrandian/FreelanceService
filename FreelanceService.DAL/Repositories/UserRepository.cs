using FreelanceService.DAL.Entities;
using FreelanceService.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            string query = "INSERT INTO Users(Email,PassHash,FirstName,LastName,Phone,DynamicSalt,RegistrationDateTime,City,Rating,Role) VALUES(@Email,@PassHash,@FirstName,@LastName,@Phone,@DynamicSalt,@RegistrationDateTime,@City,@Rating,@Role);SELECT CAST(SCOPE_IDENTITY() as int)";

            if (entity == null)
                throw new ArgumentNullException("entity");


            _context.Execute(
                query, param: new
                {
                    Email = entity.Email,
                    PassHash = entity.PassHash,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    Phone = entity.Phone,
                    DynamicSalt = entity.DynamicSalt,
                    RegistrationDateTime = entity.RegistrationDateTime,
                    City = entity.City,
                    Rating = entity.Rating,
                    Role = entity.Role

                }
            );

        }

        public User FindById(int id)
        {
            string query = "SELECT * FROM Users WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("id");

            return _context.Query<User>(
                query,
                param: new { Id = id }).FirstOrDefault();
        }

        public  User FindByEmail(string email)
        {
            string query = "Select * From Users Where Email = @email";

            if (email == null)
                throw new ArgumentNullException("email");

            return  _context.Query<User>(
                query,
                param: new { Email = email }).FirstOrDefault();
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
            string query = "UPDATE Users SET Email=@Email, PassHash=@PassHash, FirstName=@FirstName, LastName=@LastName, Phone=@Phone, DynamicSalt=@DynamicSalt, RegistrationDateTime=@RegistrationDateTime, City=@City, Rating=@Rating, Role=@Role WHERE Id=@Id";
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
                        RegistrationDateTime = entity.RegistrationDateTime,
                        City = entity.City,
                        Rating = entity.Rating,
                        Role = entity.Role
                    }
                );
        }
    }
}
