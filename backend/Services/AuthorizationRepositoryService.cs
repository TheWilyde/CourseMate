using CourseMate.Models;
using CourseMate.Repository;
using Microsoft.AspNetCore.Mvc;
using CourseMate.Models.Helping_Classes;
using CourseMate.Models.Enums;

namespace CourseMate.Services
{
    public interface IAuthorizationRepositoryService
    {
        Task<IActionResult> Login(string email, string password);
        //Task<IActionResult> AuthorizeUser(Guid userId, string role);
    }

    public class AuthorizationRepositoryService : IAuthorizationRepositoryService
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IInstructorRepository _instructorRepo;
        private readonly IAdminRepository _adminRepo;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthorizationRepositoryService(IStudentRepository studentRepo, IInstructorRepository instructorRepo, IAdminRepository adminRepo, IJwtTokenService jwtTokenService)
        {
            _studentRepo = studentRepo;
            _instructorRepo = instructorRepo;
            _adminRepo = adminRepo;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return new BadRequestObjectResult("Email and password must be provided.");
            }

            if (!EmailValidation.IsEmailFormatValid(email))
            {
                return new BadRequestObjectResult("Invalid email format.");
            }

            var entityTypeIs = EmailValidation.IdentifyEnumRole(email);

            switch (entityTypeIs)
            {
                case enumRole.Admin:
                    var admin = await _adminRepo.GetActiveAdminByEmail(email);
                    if (admin == null || admin.Password != password)
                    {
                        return new UnauthorizedObjectResult("Invalid credentials.");
                    }
                    var adminToken = _jwtTokenService.GenerateToken(admin, "Admin");
                    return new OkObjectResult(new { 
                        Token = adminToken, 
                        Role = "Admin", 
                        User = new { 
                            Id = admin.Id, 
                            FirstName = admin.FirstName, 
                            LastName = admin.LastName, 
                            Email = admin.Email 
                        } 
                    });

                case enumRole.Instructor:
                    var instructor = await _instructorRepo.GetActiveInstructorByEmail(email);
                    if (instructor == null || instructor.Password != password)
                    {
                        return new UnauthorizedObjectResult("Invalid credentials.");
                    }
                    var instructorToken = _jwtTokenService.GenerateToken(instructor, "Instructor");
                    return new OkObjectResult(new { 
                        Token = instructorToken, 
                        Role = "Instructor", 
                        User = new { 
                            Id = instructor.Id, 
                            FirstName = instructor.FirstName, 
                            LastName = instructor.LastName, 
                            Email = instructor.Email 
                        } 
                    });

                case enumRole.Student:
                    var student = await _studentRepo.GetActiveStudentByEmail(email);
                    if (student == null || student.Password != password)
                    {
                        return new UnauthorizedObjectResult("Invalid credentials.");
                    }
                    var studentToken = _jwtTokenService.GenerateToken(student, "Student");
                    return new OkObjectResult(new { 
                        Token = studentToken, 
                        Role = "Student", 
                        User = new { 
                            Id = student.Id, 
                            FirstName = student.FirstName, 
                            LastName = student.LastName, 
                            Email = student.Email,
                            Major = student.Major,
                            Year = student.Year
                        } 
                    });

                default:
                    return new BadRequestObjectResult("Unrecognized email domain.");
            }
        }
    }
}
