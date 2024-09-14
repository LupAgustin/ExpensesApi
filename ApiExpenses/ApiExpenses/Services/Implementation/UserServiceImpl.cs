using ApiExpenses.DTOs;
using ApiExpenses.Models;
using ApiExpenses.Repositories.Interface;
using ApiExpenses.Services.Interface;
using AutoMapper;

namespace ApiExpenses.Services.Implementation
{
    public class UserServiceImpl : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserServiceImpl(IUserRepository userRepository, IMapper mapper) 
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<List<UserDto>> GetAllUserAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            if(users == null)
            {
                return null;
            }
            List<UserDto> usersdto = new List<UserDto>();
            foreach (User user in users) {
                UserDto userdto = _mapper.Map<UserDto>(user);
                usersdto.Add(userdto);
            }
            
            return usersdto;
        }

    }
}
