using CourseMate.Models;
using CourseMate.Repository;
using Microsoft.AspNetCore.Mvc;
using CourseMate.Models.HelpingClasses;
using CourseMate.Models.Helping_Classes;
using CourseMate.Models.Enums;

namespace CourseMate.Services
{
    public interface IAuthorizationRepositoryService
    {
        Task<IActionResult> Login(string email, string password);
    }

    public class AuthorizationRepositoryService : IAuthorizationRepositoryService
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IInstructorRepository _instructorRepo;
        private readonly IAdminRepository _adminRepo;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthorizationRepositoryService(
            IStudentRepository studentRepo,
            IInstructorRepository instructorRepo,
            IAdminRepository adminRepo,
            IJwtTokenService jwtTokenService)
        {
            _studentRepo = studentRepo;
            _instructorRepo = instructorRepo;
            _adminRepo = adminRepo;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                string role = DetermineRole(email);

                BaseModel? user = null;

                switch (role)
                {
                    case "Student":
                        var student = await _studentRepo.GetActiveStudentByEmail(email);
                        if (student == null || student.Password != password)
                            return new UnauthorizedObjectResult("Invalid credentials.");
                        user = student;
                        break;

                    case "Instructor":
                        var instructor = await _instructorRepo.GetActiveInstructorByEmail(email);
                        if (instructor == null || instructor.Password != password)
                            return new UnauthorizedObjectResult("Invalid credentials.");
                        user = instructor;
                        break;

                    case "Admin":
                        var admin = await _adminRepo.GetActiveAdminByEmail(email);
                        if (admin == null || admin.Password != password)
                            return new UnauthorizedObjectResult("Invalid credentials.");
                        user = admin;
                        break;

                    default:
                        return new BadRequestObjectResult("Invalid email domain.");
                }

                var token = _jwtTokenService.GenerateToken(user, role);

                var response = new
                {
                    Token = token,
                    Role = role,
                    User = new
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        DateOfBirth = user.DateOfBirth,
                        Major = user is Student s ? s.Major : null,
                        Year = user is Student st ? st.Year : (int?)null,
                        Designation = user is Instructor i ? i.Designation : null,
                        Position = user is Admin a ? a.Position : null
                    }
                };

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Internal server error: {ex.Message}")
                {
                    StatusCode = 500
                };
            }
        }

        private string DetermineRole(string email)
        {
            if (email.EndsWith("@student.coursemate.com", StringComparison.OrdinalIgnoreCase))
                return "Student";
            else if (email.EndsWith("@instructor.coursemate.com", StringComparison.OrdinalIgnoreCase))
                return "Instructor";
            else if (email.EndsWith("@coursemate.com", StringComparison.OrdinalIgnoreCase))
                return "Admin";
            else
                return "Unknown";
        }
    }
}
