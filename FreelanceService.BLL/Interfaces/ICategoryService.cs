using FreelanceService.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreelanceService.BLL.Interfaces
{
    public interface ICategoryService
    {
        Task AddCategory(CategoryDTO entity);
        Task<CategoryDTO> FindCategoryById(int id);
        Task<IEnumerable<CategoryDTO>> GetAll();
        Task Update(CategoryDTO entity);
        Task CommitAsync();
    }
}
