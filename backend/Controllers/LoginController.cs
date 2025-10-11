using Microsoft.AspNetCore.Mvc;
using CourseMate.Models.Helping_Classes;
using CourseMate.Services;
using CourseMate.Models.Dtos;
using CourseMate.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseMate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IAuthorizationRepositoryService _authService;

        public LoginController(IAuthorizationRepositoryService authService)
        {
            _authService = authService;
        }

        [HttpPost("auth")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            if (string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
            {
                return BadRequest("Email and password are required.");
            }

            var result = await _authService.Login(loginDto.Email, loginDto.Password);
            return result;
        }
    }
}
