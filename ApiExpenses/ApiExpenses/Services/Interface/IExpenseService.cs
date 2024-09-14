
using ApiExpenses.DTOs;
using ApiExpenses.Models;

namespace ApiExpenses.Services.Interface
{
    public interface IExpenseService
    {
        Task<bool> CreateExpenseAsync(ExpenseDtoCreate expenseDto);
        Task<bool> DeleteExpenseAsync(string id);
        Task<List<Expense>> GetByDate(DateTime startDate, DateTime endDate);
        Task<List<ExpenseDto>> GetExpensesAsync();
        Task<List<UserExpenseDto>> GetUserExpensesAsync();
        Task<bool> UpdateExpenseAsync(ExpenseDtoUpdate expenseDto);
    }
}
