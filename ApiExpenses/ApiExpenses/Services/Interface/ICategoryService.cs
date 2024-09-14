
using ApiExpenses.Models;

namespace ApiExpenses.Services.Interface
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategoriesAsync();
    }
}
