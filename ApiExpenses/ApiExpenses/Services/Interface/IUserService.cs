
using ApiExpenses.DTOs;
using ApiExpenses.Models;

namespace ApiExpenses.Services.Interface
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUserAsync();
    }
}
