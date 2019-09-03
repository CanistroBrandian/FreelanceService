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
    public class CategoryRepository : ICategoryRepository
    {
        protected readonly IDbContext _context;
        public CategoryRepository(IDbContext context)
        {
            _context = context;
        }


        public void AddCategory(Category entity)
        {
            string query = "INSERT INTO Categories VALUES(@Id,@Name);SELECT CAST(SCOPE_IDENTITY() as int)";

            if (entity == null)
                throw new ArgumentNullException("entity");


            _context.Execute(
                query, param: entity
            );

        }

        public Category Find(int id)
        {
            string query = "SELECT * FROM Categories WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("id");

            return _context.Query<Category>(
                query,
                param: new { Id = id }).FirstOrDefault();
        }

        public IEnumerable<Category> GetAll()
        {
            string query = "SELECT * FROM Categories";
            return _context.Query<Category>(query);
        }

        public void Remove(int id)
        {
            string query = "DELETE FROM Categories WHERE Id = @id";

            if (id == 0)
                throw new ArgumentNullException("entity");
            _context.Execute(query);

        }

        public void Update(Category entity)
        {
            string query = "UPDATE Categorys SET Id=@Id, Name=@Name WHERE Id = @Id";
            _context.Execute(query,
                    param: entity);
        }
    }
}

