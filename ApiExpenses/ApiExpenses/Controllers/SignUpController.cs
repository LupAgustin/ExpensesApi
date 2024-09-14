using ApiExpenses.DTOs;
using ApiExpenses.Models;
using ApiExpenses.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiExpenses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly ISignUpService _signUpService;

        public SignUpController(ISignUpService signUpService) 
        {
            _signUpService = signUpService;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Register([FromBody] LoginDto request)
        {
            var result = await _signUpService.SignUpAsync(request);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(true);
        }

    }
}
