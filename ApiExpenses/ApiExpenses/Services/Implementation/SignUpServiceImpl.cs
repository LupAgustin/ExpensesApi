using ApiExpenses.DTOs;
using ApiExpenses.Models;
using ApiExpenses.Repositories.Interface;
using ApiExpenses.Services.Interface;
using AutoMapper;

namespace ApiExpenses.Services.Implementation
{
    public class SignUpServiceImpl : ISignUpService
    {
        private readonly ISignUpRepository _signUpRepository;
        private readonly IMapper _mapper;

        public SignUpServiceImpl(ISignUpRepository signUpRepository, IMapper mapper)
        {
            _signUpRepository = signUpRepository;
            _mapper = mapper;
        }
        public async Task<UserDto> SignUpAsync(LoginDto request)
        {
            var result = await _signUpRepository.ValidateSignUpAsync(request);
            if (result == null) 
            {
                User user = new User();
                Guid id = Guid.NewGuid();
                user.Id = id.ToString();
                user.Username = request.Username;
                user.Password = request.Password;
                user.Role = "User";
                user.Expenses = new List<Expense>();
                await _signUpRepository.SaveUserAsync(user);
                UserDto userDto = _mapper.Map<UserDto>(user);
                return userDto;
            }
            return null;
        }
    }
}
