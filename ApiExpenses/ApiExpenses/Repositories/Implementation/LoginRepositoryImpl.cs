using ApiExpenses.DTOs;
using ApiExpenses.Models;
using ApiExpenses.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiExpenses.Repositories.Implementation
{
    public class LoginRepositoryImpl : ILoginRepository
    {
        private readonly ApiExpensesContext _context;

        public LoginRepositoryImpl(ApiExpensesContext context) {
            _context = context;
        }

        public async Task<User> ValidateLoginAsync(LoginDto request)
        {
            return await _context.Users.FirstOrDefaultAsync(x =>
                                     x.Username == request.Username &&
                                     x.Password == request.Password);            
        }
    }
}
