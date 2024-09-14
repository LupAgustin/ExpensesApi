
using ApiExpenses.Models;

namespace ApiExpenses.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();
    }
}
