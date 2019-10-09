﻿using FreelanceService.DAL.Entities;
using FreelanceService.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace FreelanceService.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        protected readonly IDbContext _context;
        public CategoryRepository(IDbContext context)
        {
            _context = context;
        }
        
        public async Task AddCategory(Category entity)
        {
            string query = "INSERT INTO Categories VALUES(@Id,@Name);SELECT CAST(SCOPE_IDENTITY() as int)";
            await _context.ExecuteAsync(query, param: entity);

        }

        public async Task<Category> FindById(int id)
        {
            string query = "SELECT * FROM Categories WHERE Id = @id";

            return  await _context.QueryFirst<Category>(
                query,
                param: new { Id = id });
        }

        public async Task< IEnumerable<Category>> GetAll()
        {
            string query = "SELECT * FROM Categories";
            return await _context.Query<Category>(query);
        }

        public async Task Remove(int id)
        {
            string query = "DELETE FROM Categories WHERE Id = @id";
            await _context.ExecuteAsync(query);

        }

        public async Task Update(Category entity)
        {
            string query = "UPDATE Categorys SET Id=@Id, Name=@Name WHERE Id = @Id";
           await _context.ExecuteAsync(query,
                    param: entity);
        }
    }
}

