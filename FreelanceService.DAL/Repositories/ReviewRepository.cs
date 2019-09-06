using Dapper;
using FreelanceService.DAL.Concrate;
using FreelanceService.DAL.Entities;
using FreelanceService.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace FreelanceService.DAL.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        protected readonly IDbContext _context;
        public ReviewRepository(IDbContext context)
        {
            _context = context;
        }


        public async Task AddReview(Review entity)
        {
            string query = "INSERT INTO Reviews VALUES(@Id, @UserId, @Name, @Description, @DateOfWriting, @Feedback, @Rating); SELECT CAST(SCOPE_IDENTITY() as int)";

            if (entity == null)
                throw new ArgumentNullException("entity");


           await _context.Execute(
                query, param: entity
            );

        }

        public async Task<Review> FindById(int id)
        {
            string query = "SELECT * FROM Reviews WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("id");

            return await _context.QueryFirst<Review>(
                query,
                param: new { Id = id });
        }

        public async Task<IEnumerable<Review>> GetAll()
        {
            string query = "SELECT * FROM Reviews";
            return await _context.Query<Review>(query);
        }

        public async Task Remove(int id)
        {
            string query = "DELETE FROM Reviews WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("entity");
           await _context.Execute(query);

        }

        public async Task Update(Review entity)
        {
            string query = "UPDATE Reviews SET Id=@Id, UserId=@UserId, Name=@Name, Description=@Description, DateOfWriting=@DateOfWriting, Feedback=@Feedback, Rating=@Rating WHERE Id = @Id";
           await _context.Execute(query, param: entity);
        }
    }
}