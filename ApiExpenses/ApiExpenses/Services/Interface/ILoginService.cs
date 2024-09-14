
using ApiExpenses.DTOs;
using ApiExpenses.Models;

namespace ApiExpenses.Services.Interface
{
    public interface ILoginService
    {
        Task<string> LoginAsync(LoginDto request);
    }
}
