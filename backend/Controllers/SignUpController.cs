using CourseMate.Models.Dtos;
using CourseMate.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseMate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly IAuthorizationRepositoryService _authService;
        public SignUpController(IAuthorizationRepositoryService authService) => _authService = authService;


        [HttpPost("auth")]
        public async Task<IActionResult> SignUp(SignUpRequestDto signupDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            await _authService.SignUp(signupDto);
            return Ok("SignUp Successful");
        }
    }
}
