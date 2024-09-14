using ApiExpenses.DTOs;
using ApiExpenses.Models;

namespace ApiExpenses.Repositories.Interface
{
    public interface ILoginRepository
    {
        Task<User> ValidateLoginAsync(LoginDto request);
    }
}
