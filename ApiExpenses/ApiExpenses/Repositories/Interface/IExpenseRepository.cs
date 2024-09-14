using ApiExpenses.Models;

namespace ApiExpenses.Repositories.Interface
{
    public interface IExpenseRepository
    {
        Task<bool> DeleteExpense(string id);
        Task<List<Expense>> GetExpenses();
        Task<List<Expense>> GetExpensesByDateAsync(DateTime startDate, DateTime endDate);
        Task<List<Expense>> GetUserExpenses(string id);
        Task<bool> SaveExpense(Expense expense);
        Task<bool> UpdateExpense(Expense expense);
    }
}
