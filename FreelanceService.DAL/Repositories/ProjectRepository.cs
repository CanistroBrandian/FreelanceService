using FreelanceService.DAL.Entities;
using FreelanceService.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace FreelanceService.DAL.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        protected readonly IDbContext _context;
        public ProjectRepository(IDbContext context)
        {
            _context = context;
        }


        public async Task AddProject(Project entity)
        {
            string query = "INSERT INTO Projects VALUES(@Id,@Name,@Description,@Image);SELECT CAST(SCOPE_IDENTITY() as int)";
           await _context.ExecuteAsync(query, param: entity );

        }

        public async Task<Project> FindById(int id)
        {
            string query = "SELECT * FROM Projects WHERE Id = @id";
            return await _context.QueryFirst<Project>(
                query,
                param: new { Id = id });
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            string query = "SELECT * FROM Projects";
            return await _context.Query<Project>(query);
        }

        public async Task Remove(int id)
        {
            string query = "DELETE FROM Projects WHERE Id = @id";
           await _context.ExecuteAsync(query);

        }

        public async Task Update(Project entity)
        {
            string query = "UPDATE Projects SET Id=@Id, Name=@Name, Description=@Description, Image=@Image WHERE Id = @Id";

            await _context.ExecuteAsync(query,
                    param: entity);
        }
    }
}