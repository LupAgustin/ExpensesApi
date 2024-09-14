
using ApiExpenses.DTOs;

namespace ApiExpenses.Services.Interface
{
    public interface ISignUpService
    {
        Task<UserDto> SignUpAsync(LoginDto request);
    }
}
