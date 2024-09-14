using ApiExpenses.Models;
using ApiExpenses.Repositories.Interface;
using ApiExpenses.Services.Interface;

namespace ApiExpenses.Services.Implementation
{
    public class CategoryServiceImpl : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryServiceImpl(ICategoryRepository categoryRepository) 
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetCategories();            
        }
    }
}
