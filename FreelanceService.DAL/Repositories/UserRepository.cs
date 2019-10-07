
using FreelanceService.Common.Enum;
using FreelanceService.DAL.Entities;
using FreelanceService.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace FreelanceService.DAL.Repositories
{
    /// <summary>
    /// User Repository. Implemented CRUD operations in the Users table
    /// </summary>
    public class UserRepository : IUserRepository
    {

        protected readonly IDbContext _context;

        public UserRepository(IDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Send query for adding a new entry to the table Users
        /// </summary>
        /// <param name="entity">Accepts an entity of type User</param>
        /// <returns>Void</returns>   
        public async Task AddUser(User entity)
        {
            string query = "INSERT INTO Users(Email,PassHash,FirstName,LastName,Phone,DynamicSalt,City,Rating,Role) VALUES(@Email,@PassHash,@FirstName,@LastName,@Phone,@DynamicSalt,@City,@Rating,@Role);SELECT CAST(SCOPE_IDENTITY() as int)";

            await _context.ExecuteAsync(
                query, param: new
                {
                    Email = entity.Email,
                    PassHash = entity.PassHash,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    Phone = entity.Phone,
                    DynamicSalt = entity.DynamicSalt,
                    City = entity.City,
                    Rating = entity.Rating,
                    Role = entity.Role
                }
            );


        }

        /// <summary>
        /// Send query to serch fields in table Users equal to id and returns value
        /// </summary>
        /// <param name="id"> id is of type int</param>
        /// <returns>Returns a user with type User</returns>
        public async Task<User> FindUserById(int id)
        {
            string query = "SELECT * FROM Users WHERE Id = @id";

            return await _context.QueryFirst<User>(
                query,
                param: new { Id = id });
        }


        /// <summary>
        /// Search value fields in table Users equal to email and returns value
        /// </summary>
        /// <param name="email"> email is of type string</param>
        /// <returns>Returns a user with type User</returns>
        public async Task<User> FindByEmail(string email)
        {
            string query = "Select * From Users Where Email = @email";

            return await _context.QueryFirst<User>(
                query,
                param: new { Email = email });
        }

        /// <summary>
        /// Send query to search all entries in the Users table
        /// </summary>
        /// <returns> Returns all entries ​​in an IEnumerable User </returns>
        public async Task<IEnumerable<User>> GetAll()
        {
            string query = "SELECT * FROM Users";
            return await _context.Query<User>(query);
        }

        public async Task<IEnumerable<User>> GetAllUsersExecutorsOfResponse(List<int> listUserExecutorId)
        {
            var usersExecutors = await GetAll();
            foreach (var item in listUserExecutorId)
            {
                string query = "SELECT * FROM Users WHERE Id=@Id";
                usersExecutors = await _context.Query<User>(query, param: new { Id = item });
            }
            return usersExecutors;
        }


        public async Task<IEnumerable<User>> GetAllExecutor()
        {
            string query = "SELECT * FROM Users WHERE Role=@Role";
            return await _context.Query<User>(query, param: new { Role = (int)RoleEnum.Executor });
        }

        /// <summary>
        ///Send query to delete the Users table field equal to Id
        /// </summary>
        /// <param name="id">id is of type int</param>
        /// <returns>void</returns>
        public async Task Remove(int id)
        {
            string query = "DELETE FROM Users WHERE Id = @id";
            await _context.ExecuteAsync(query);
        }

        /// <summary>
        /// Send quey to update the values ​​of the fields of the Users table from the entity parameters
        /// </summary>
        /// <param name="entity">Accepts an entity of type User</param>
        /// <returns>void</returns>
        public async Task Update(User entity)
        {
            string query = "UPDATE Users SET FirstName=@FirstName, LastName=@LastName, Phone=@Phone, City=@City,Role=@Role, VerifyCodeForResetPass=@VerifyCodeForResetPass WHERE Id=@Id";
            await _context.ExecuteAsync(query,
                    param: new
                    {
                        Id = entity.Id,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        Phone = entity.Phone,
                        City = entity.City,
                        Role = entity.Role,
                        VerifyCodeForResetPass = entity.VerifyCodeForResetPass
                    }
                );
        }

        public async Task ResetPassword(User entity)
        {
            string query = "UPDATE Users SET PassHash = @PassHash, VerifyCodeForResetPass=null WHERE Id=@Id";
            await _context.ExecuteAsync(query,
                    param: new
                    {
                        Id = entity.Id,
                        PassHash = entity.PassHash,
                    }
                );
        }
    }
}
