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

        [HttpPost]
        [Route("auth")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            var result = await _authService.Login(loginDto.Email, loginDto.Password);
            
            return result;
        }
    }
}
