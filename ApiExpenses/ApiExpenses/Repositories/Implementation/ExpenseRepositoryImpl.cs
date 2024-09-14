using ApiExpenses.Models;
using ApiExpenses.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiExpenses.Repositories.Implementation
{
    public class ExpenseRepositoryImpl : IExpenseRepository
    {
        private readonly ApiExpensesContext _context;

        public ExpenseRepositoryImpl(ApiExpensesContext context) 
        {
            _context = context;
        }

        public async Task<bool> DeleteExpense(string id)
        {
            bool result = false;
            try
            {
                var expense = await _context.Expenses.FirstOrDefaultAsync(x => x.Id == id);
                if (expense != null)
                {
                    _context.Expenses.Remove(expense);
                    await _context.SaveChangesAsync();
                    result = true;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public async Task<List<Expense>> GetExpenses()
        {
            return await _context.Expenses.Include(x => x.User).Include(y => y.Category).ToListAsync();
        }

        public async Task<List<Expense>> GetExpensesByDateAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Expenses.Where(x=> x.Date >= startDate && x.Date <= endDate).ToListAsync();
        }

        public async Task<List<Expense>> GetUserExpenses(string id)
        {
            return await _context.Expenses.Where(x=> x.UserId == id).Include(y => y.Category).ToListAsync();
        }

        public async Task<bool> SaveExpense(Expense expense)
        {
            bool result = false;
            try
            {
                await _context.Expenses.AddAsync(expense);
                await _context.SaveChangesAsync();
                result = true;
            }
            catch{
                result = false;
            }
            return result;
        }

        public async Task<bool> UpdateExpense(Expense expenseUpdated)
        {
            bool result = false;
            try
            {
                _context.Expenses.Update(expenseUpdated);
                await _context.SaveChangesAsync();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}
