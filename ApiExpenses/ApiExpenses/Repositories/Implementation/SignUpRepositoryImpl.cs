using ApiExpenses.DTOs;
using ApiExpenses.Models;
using ApiExpenses.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiExpenses.Repositories.Implementation
{
    public class SignUpRepositoryImpl : ISignUpRepository
    {
        private readonly ApiExpensesContext _context;

        public SignUpRepositoryImpl(ApiExpensesContext context) 
        {
            _context = context;
        }

        public async Task SaveUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> ValidateSignUpAsync(LoginDto request)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Username == request.Username);            
        }
    }
}
