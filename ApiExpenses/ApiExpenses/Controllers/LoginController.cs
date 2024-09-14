using ApiExpenses.DTOs;
using ApiExpenses.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ApiExpenses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService) 
        {
            _loginService = loginService;
        }
        [HttpPost]
        public async Task<ActionResult<string>> Register([FromBody] LoginDto request)
        {
            var result = await _loginService.LoginAsync(request);
            if (result.IsNullOrEmpty())
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
