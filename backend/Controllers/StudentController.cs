using CourseMate.Models.Dtos;
using CourseMate.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseMate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepositoryService _studentService;

        public StudentController(IStudentRepositoryService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddNewStudent([FromBody] StudentDto? studentDto)
        {
            if (studentDto == null)
                return BadRequest("Student data is required.");

            var age = GetAge(studentDto.DateOfBirth);

            var validationResult = studentDto switch
            {
                _ when !AllFieldsArePresent(studentDto)
                    => "Missing required fields.",

                _ when studentDto.PhoneNumber?.Length != 11 ||
                       !(studentDto.PhoneNumber?.All(char.IsDigit) ?? false)
                    => "Phone number must be exactly 11 digits.",

                _ when studentDto.DateOfBirth > DateTime.Now || age < 18
                    => "Date of birth cannot be in the future or less than 18.",

                _ => null
            };

            if (validationResult != null)
                return BadRequest(validationResult);

            var result = await _studentService.AddNewStudent(studentDto);
            
            if (!result.IsSuccess)
                return Conflict(result.ErrorMessage); // 409 Conflict for existing student

            return Ok("Student added successfully.");
        }

        private int GetAge(DateTime dob)
        {
            var today = DateTime.Today;
            var age = today.Year - dob.Year;
            if (dob.Date > today.AddYears(-age)) age--;
            return age;
        }

        private bool AllFieldsArePresent(StudentDto studentDto)
        {
            return !(string.IsNullOrEmpty(studentDto.FirstName) ||
                     string.IsNullOrEmpty(studentDto.LastName) ||
                     string.IsNullOrEmpty(studentDto.Email) ||
                     string.IsNullOrEmpty(studentDto.Password) ||
                     string.IsNullOrEmpty(studentDto.PhoneNumber) ||
                     string.IsNullOrEmpty(studentDto.Major));
        }
    }
}
