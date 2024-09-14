using ApiExpenses.DTOs;
using ApiExpenses.Models;
using ApiExpenses.Repositories.Interface;
using ApiExpenses.Services.Interface;
using AutoMapper;
using System.Security.Claims;

namespace ApiExpenses.Services.Implementation
{
    public class ExpenseServiceImpl : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExpenseServiceImpl(IExpenseRepository expenseRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) 
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CreateExpenseAsync(ExpenseDtoCreate expenseDto)
        {
            Expense expense = _mapper.Map<Expense>(expenseDto);
            expense.Id = Guid.NewGuid().ToString();
            return await _expenseRepository.SaveExpense(expense);
            
        }

        public async Task<bool> DeleteExpenseAsync(string id)
        {
            return await _expenseRepository.DeleteExpense(id);
        }

        public async Task<List<Expense>> GetByDate(DateTime startDate, DateTime endDate)
        {
            return await _expenseRepository.GetExpensesByDateAsync(startDate, endDate);
        }

        public async Task<List<ExpenseDto>> GetExpensesAsync()
        {
            var expenses = await _expenseRepository.GetExpenses();
            if(expenses == null) { return null; }
            List<ExpenseDto> expensesDto = new List<ExpenseDto>();
            foreach (var expense in expenses) 
            {
                UserDto userDto = _mapper.Map<UserDto>(expense.User);
                CategoryDto categoryDto = _mapper.Map<CategoryDto>(expense.Category);
                ExpenseDto expenseDto = _mapper.Map<ExpenseDto>(expense);
                expenseDto.User = userDto;
                expenseDto.Category = categoryDto;
                
                expensesDto.Add(expenseDto);
            }
            return expensesDto;
        }

        public async Task<List<UserExpenseDto>> GetUserExpensesAsync()
        {
            if (_httpContextAccessor.HttpContext.User == null)
            {
                return null;
            }

            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            var userId = userIdClaim.Value.ToString();

            var expenses = await _expenseRepository.GetUserExpenses(userId);
            if (expenses == null) { return null; }
            List<UserExpenseDto> expensesDto = new List<UserExpenseDto>();
            foreach (var expense in expenses)
            {
                CategoryDto categoryDto = _mapper.Map<CategoryDto>(expense.Category);
                UserExpenseDto expenseDto = _mapper.Map<UserExpenseDto>(expense);
                expenseDto.Category = categoryDto;

                expensesDto.Add(expenseDto);
            }
            return expensesDto;
        }

        public async Task<bool> UpdateExpenseAsync(ExpenseDtoUpdate expenseDto)
        {
            Expense expense = _mapper.Map<Expense>(expenseDto);
            return await _expenseRepository.UpdateExpense(expense);
        }
    }
}
