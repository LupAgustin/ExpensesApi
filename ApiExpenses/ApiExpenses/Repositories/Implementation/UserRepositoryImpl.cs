using ApiExpenses.Models;
using ApiExpenses.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiExpenses.Repositories.Implementation
{
    public class UserRepositoryImpl : IUserRepository
    {
        private readonly ApiExpensesContext _context;

        public UserRepositoryImpl(ApiExpensesContext context) {
            _context = context;
        }
        public Task<List<User>> GetAllUsersAsync()
        {
            var users = _context.Users.ToListAsync();
            return users;
        }

    }
}
