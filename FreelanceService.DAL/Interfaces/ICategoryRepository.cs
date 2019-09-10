using FreelanceService.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace FreelanceService.DAL.Interfaces
{
    public interface ICategoryRepository
    {
        Task AddCategory(Category entity);
        Task <IEnumerable<Category>> GetAll();
        Task<Category> FindById(int id);
        Task Remove(int id);
        Task Update(Category entity);
    }
}
