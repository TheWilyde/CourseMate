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
        public AuthorizationRepositoryService(IStudentRepository studentRepo, IInstructorRepository instructorRepo, IAdminRepository adminRepo)
        {
            _studentRepo = studentRepo;
            _instructorRepo = instructorRepo;
            _adminRepo = adminRepo;
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
                        return new UnauthorizedResult();
                    }
                    return new OkObjectResult(new { Role = "Admin", User = admin });

                case enumRole.Instructor:
                     var instructor = await _instructorRepo.GetActiveInstructorByEmail(email);
                    if (instructor == null || instructor.Password != password)
                    {
                        return new UnauthorizedResult();
                    }
                    return new OkObjectResult(new { Role = "Instructor", User = instructor });

                case enumRole.Student:
                    var student = await _studentRepo.GetActiveStudentByEmail(email);
                    if (student == null || student.Password != password)
                    {
                        return new UnauthorizedResult();
                    }
                    return new OkObjectResult(new { Role = "Student", User = student });

                    default:
                    return new BadRequestObjectResult("Unrecognized email domain.");
            }

        }

    }
}
