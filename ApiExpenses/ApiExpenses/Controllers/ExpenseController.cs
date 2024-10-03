using ApiExpenses.DTOs;
using ApiExpenses.Models;
using ApiExpenses.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;

namespace ApiExpenses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User, Admin")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService) 
        {
            _expenseService = expenseService;
        }

        [HttpGet("/AllExpensesAdmin")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<ExpenseDto>>?> GetEveryExpense() 
        {
            var expenses = await _expenseService.GetExpensesAsync();
            if (expenses == null) { return null; }
            return Ok(expenses);
        }

        [HttpGet("/AllUserExpenses")]
        public async Task<ActionResult<List<UserExpenseDto>>?> GetAllExpenses()
        {
            var expenses = await _expenseService.GetUserExpensesAsync();
            if (expenses == null) { return null; }
            return Ok(expenses);
        }

        [HttpGet("/GetExpensesByDate")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Expense>>?> GetExpensesByDate(DateTime startDate, DateTime endDate) 
        {
            var expenses = await _expenseService.GetByDate(startDate, endDate);
            if (expenses == null) { return null; } 
            return Ok(expenses);
        }

        [HttpPost("Expenses/CreateExpense")]
        public async Task<ActionResult> CreateExpense([FromBody] ExpenseDtoCreate expenseDto) 
        { 
            bool expenseResult = await _expenseService.CreateExpenseAsync(expenseDto);
            if (!expenseResult) { return BadRequest(); }
            return Ok();
        }

        [HttpPut("/UpdateExpense")]
        public async Task<ActionResult> UpdateExpense([FromBody] ExpenseDtoUpdate expenseDto) 
        {
            bool expenseResult = await _expenseService.UpdateExpenseAsync(expenseDto);
            if (!expenseResult) { return BadRequest(); }
            return Ok();
        }

        [HttpDelete("/DeleteExpense")]
        public async Task<ActionResult> DeleteExpenseById(string id) 
        {
            bool expenseResult = await _expenseService.DeleteExpenseAsync(id);
            if (!expenseResult) { return BadRequest(); }
            return Ok();
        }
        
    }
}
