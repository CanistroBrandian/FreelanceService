using FreelanceService.DAL.Entities;
using System.Collections.Generic;

namespace FreelanceService.DAL.Interfaces
{
    public interface ICategoryRepository
    {
        void AddCategory(Category entity);
        IEnumerable<Category> GetAll();
        Category Find(int id);
        void Remove(int id);
        void Update(Category entity);
    }
}
