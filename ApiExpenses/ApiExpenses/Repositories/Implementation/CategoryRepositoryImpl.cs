using ApiExpenses.Models;
using ApiExpenses.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiExpenses.Repositories.Implementation
{
    public class CategoryRepositoryImpl : ICategoryRepository
    {
        private readonly ApiExpensesContext _context;

        public CategoryRepositoryImpl(ApiExpensesContext context) 
        {
            _context = context;
        }
        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();            
        }
    }
}
