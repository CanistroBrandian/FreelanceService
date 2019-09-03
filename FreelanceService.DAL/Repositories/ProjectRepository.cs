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
    public class ProjectRepository : IProjectRepository
    {
        protected readonly IDbContext _context;
        public ProjectRepository(IDbContext context)
        {
            _context = context;
        }


        public void AddProject(Project entity)
        {
            string query = "INSERT INTO Projects VALUES(@Id,@Name,@Description,@Image);SELECT CAST(SCOPE_IDENTITY() as int)";

            if (entity == null)
                throw new ArgumentNullException("entity");


            _context.Execute(
                query, param: entity
            );

        }

        public Project Find(int id)
        {
            string query = "SELECT * FROM Projects WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("id");

            return _context.Query<Project>(
                query,
                param: new { Id = id }).FirstOrDefault();
        }

        public IEnumerable<Project> GetAll()
        {
            string query = "SELECT * FROM Projects";
            return _context.Query<Project>(query);
        }

        public void Remove(int id)
        {
            string query = "DELETE FROM Projects WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("entity");
            _context.Execute(query);

        }

        public void Update(Project entity)
        {
            string query = "UPDATE Projects SET Id=@Id, Name=@Name, Description=@Description, Image=@Image WHERE Id = @Id";

            _context.Execute(query,
                    param: entity);
        }
    }
}