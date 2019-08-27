using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FreelanceService.DAL.Entities;

namespace FreelanceService.DAL.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {

        public UserRepository(IDbConnection connection)
           : base(connection)
        {
        }

        public void AddUser(User entity)
        {
            string query = "INSERT INTO Users({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10} VALUES(@{0},@{1},@{2},@{3},@{4},@{5},@{6},@{7},@{8},@{9},@{10}); SELECT SCOPE_IDENTITY()", 
                Id, Email, PassHash, FirstName, LastName, Phone, DynamicSalt, DateRegistration, Image, Rating, Role ;

            if (entity == null)
                throw new ArgumentNullException("entity");

            entity.Id = Connection.ExecuteScalar<int>(
                query,
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

        public User Find(int id)
        {
            string query = "SELECT * FROM Users WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("id");

            return Connection.Query<User>(
                query,
                param: new { Id = id }
            ).FirstOrDefault();
        }

        public IEnumerable<User> GetAll()
        {
            string query = "SELECT * FROM Users";
            return Connection.Query<User>(query);
        }

        public void Remove(int id)
        {
            string query = "DELETE FROM Users WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("entity");
            Connection.Execute(query);

        }

        public void Update(User entity)
        {
            string query = "UPDATE Users SET {0}=@{0},{1}=@{1},{2}=@{2},{3}=@{3},{4}=@{4},{5}=@{5},{6}=@{6},{7}=@{7},{8}=@{8},{9}=@{9},{10}=@{10} WHERE Id = @Id",
                 Id, Email, PassHash, FirstName, LastName, Phone, DynamicSalt, DateRegistration, Image, Rating, Role;
            Connection.Execute(query,
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
