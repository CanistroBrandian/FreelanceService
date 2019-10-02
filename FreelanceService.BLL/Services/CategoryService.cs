using AutoMapper;
using FreelanceService.BLL.DTO;
using FreelanceService.BLL.Interfaces;
using FreelanceService.DAL.Entities;
using FreelanceService.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreelanceService.BLL.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task AddCategory(CategoryDTO entity)
        {
            var category = _mapper.Map<CategoryDTO, Category>(entity);
            await _uow.CategoryRepos.AddCategory(category);

        }


        public async Task<CategoryDTO> FindCategoryById(int id)
        {

            var category = await _uow.CategoryRepos.FindById(id);
            return _mapper.Map<Category, CategoryDTO>(category);
        }

        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {
            var result = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(await _uow.CategoryRepos.GetAll());
            return result;
        }

        public async Task Update(CategoryDTO entity)
        {

            var category = _mapper.Map<CategoryDTO, Category>(entity);
            await _uow.CategoryRepos.Update(category);
        }

        public async Task Remove(CategoryDTO entity)
        {
            await _uow.CategoryRepos.Remove(entity.Id);
        }

        public async Task CommitAsync()
        {
            await _uow.CommitAsync();
        }
    }
}
