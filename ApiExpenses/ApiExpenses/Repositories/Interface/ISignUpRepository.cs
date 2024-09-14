using ApiExpenses.DTOs;
using ApiExpenses.Models;

namespace ApiExpenses.Repositories.Interface
{
    public interface ISignUpRepository
    {
        Task SaveUserAsync(User user);
        Task<User> ValidateSignUpAsync(LoginDto request);
    }
}
