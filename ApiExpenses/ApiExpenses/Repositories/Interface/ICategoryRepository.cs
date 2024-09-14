using ApiExpenses.Models;

namespace ApiExpenses.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
    }
}
