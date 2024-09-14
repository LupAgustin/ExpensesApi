using ApiExpenses.DTOs;
using ApiExpenses.Models;
using ApiExpenses.Repositories.Interface;
using ApiExpenses.Services.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiExpenses.Services.Implementation
{
    public class LoginServiceImpl : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IConfiguration _configuration;

        public LoginServiceImpl(ILoginRepository loginRepository, IConfiguration configuration) 
        {
            _loginRepository = loginRepository;
            _configuration = configuration;
        }

        public async Task<string> LoginAsync(LoginDto request)
        {
            User user = await _loginRepository.ValidateLoginAsync(request);
            if (user == null) 
            {
                return string.Empty;
            }

            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var tokenDescription = new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Secret").Value!)),
                    SecurityAlgorithms.HmacSha256Signature)
                );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescription);

            return token;
        }
    }
}
