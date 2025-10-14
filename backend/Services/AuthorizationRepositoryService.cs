using CourseMate.Models;
using CourseMate.Models.Dtos;
using CourseMate.Models.Enums;
using CourseMate.Models.HelpingClasses;
using CourseMate.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CourseMate.Services
{
    public interface IAuthorizationRepositoryService
    {
        Task<IActionResult> Login(LoginRequestDto loginDto);
        Task<IActionResult> SignUp(SignUpRequestDto signUpDto);
    }

    public class AuthorizationRepositoryService : IAuthorizationRepositoryService
    {
        #region DI
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
        #endregion

        #region Role Configuration
        private static readonly Dictionary<string, enumRole> _emailDomainToRole = new(StringComparer.OrdinalIgnoreCase)
        {
            { "@student.coursemate.com", enumRole.Student },
            { "@instructor.coursemate.com", enumRole.Instructor },
            { "@coursemate.com", enumRole.Admin }
        };

        private Dictionary<enumRole, Func<string, Task<BaseModel?>>> IdentifyUserByEmail => new()
        {
            { enumRole.Student, async email => await _studentRepo.GetActiveStudentByEmail(email) },
            { enumRole.Instructor, async email => await _instructorRepo.GetActiveInstructorByEmail(email) },
            { enumRole.Admin, async email => await _adminRepo.GetActiveAdminByEmail(email) }
        };

        private Dictionary<enumRole, Func<SignUpRequestDto, Task<BaseModel>>> CreateUserActions => new()
        {
            {
                enumRole.Student,
                async dto =>
                {
                    var student = new Student
                    {
                        Id = Guid.NewGuid(),
                        FirstName = dto.FirstName,
                        LastName = dto.LastName,
                        Email = dto.Email,
                        Password = PasswordHelper.HashPassword(dto.Password),
                        PhoneNumber = dto.PhoneNumber,
                        DateOfBirth = dto.DateOfBirth,
                        Major = dto.Major ?? "Undeclared",
                        Year = dto.Year ?? DateTime.UtcNow.Year,
                        Role = (int)enumRole.Student,
                        IsActive = (int)enumStatus.Active,
                        IsDeleted = false,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        DepartmentId = Guid.Empty, // TODO: Get from DTO or default
                        DegreeId = Guid.Empty // TODO: Get from DTO or default
                    };
                    await _studentRepo.AddStudent(student);
                    return student;
                }
            },
            {
                enumRole.Instructor,
                async dto =>
                {
                    var instructor = new Instructor
                    {
                        Id = Guid.NewGuid(),
                        FirstName = dto.FirstName,
                        LastName = dto.LastName,
                        Email = dto.Email,
                        Password = PasswordHelper.HashPassword(dto.Password),
                        PhoneNumber = dto.PhoneNumber,
                        DateOfBirth = dto.DateOfBirth,
                        Designation = dto.Designation ?? "Instructor",
                        Role = (int)enumRole.Instructor,
                        IsActive = (int)enumStatus.Active,
                        IsDeleted = false,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        DepartmentId = Guid.Empty // TODO: Get from DTO or default
                    };
                    // TODO: Uncomment when AddInstructor is implemented
                    // await _instructorRepo.AddInstructor(instructor);
                    return instructor;
                }
            }
        };
        #endregion

        #region Login & SignUp
        public async Task<IActionResult> Login(LoginRequestDto loginDto)
        {
            try
            {
                var roleResult = DetermineRole(loginDto.Email);
                if (!roleResult.HasValue)
                    return new BadRequestObjectResult("Invalid email domain.");

                var role = roleResult.Value;

                var getUserAction = IdentifyUserByEmail[role];
                var user = await getUserAction(loginDto.Email);

                if (user == null)
                    return new UnauthorizedObjectResult("Invalid credentials.");

                if (!PasswordHelper.VerifyPassword(loginDto.Password, user.Password))
                    return new UnauthorizedObjectResult("Invalid credentials.");

                var token = _jwtTokenService.GenerateToken(user, role.ToString());

                return new OkObjectResult(new
                {
                    Token = token,
                    Role = role,
                    User = BuildUserResponse(user)
                });
            }
            catch (KeyNotFoundException)
            {
                return new BadRequestObjectResult("Invalid role configuration.");
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Internal server error: {ex.Message}")
                {
                    StatusCode = 500
                };
            }
        }

        public async Task<IActionResult> SignUp(SignUpRequestDto signUpDto)
        {
            try
            {
                var roleResult = DetermineRole(signUpDto.Email);
                if (!roleResult.HasValue)
                    return new BadRequestObjectResult("Invalid email domain for sign-up.");

                var role = roleResult.Value;

                if (role == enumRole.Admin)
                    return new BadRequestObjectResult("Admin sign-up is not allowed.");

                var getUserAction = IdentifyUserByEmail[role];
                var existingUser = await getUserAction(signUpDto.Email);
                if (existingUser != null)
                    return new BadRequestObjectResult("A user with this email already exists.");

                var createUserAction = CreateUserActions[role];
                var newUser = await createUserAction(signUpDto);

                return new OkObjectResult(new
                {
                    Message = $"{role} registered successfully.",
                    UserId = newUser.Id,
                    Email = newUser.Email
                });
            }
            catch (KeyNotFoundException)
            {
                return new BadRequestObjectResult("Sign-up not configured for this role.");
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Internal server error: {ex.Message}")
                {
                    StatusCode = 500
                };
            }
        }
        #endregion

        #region Helper Methods
        private enumRole? DetermineRole(string email)
        {
            foreach (var (domain, role) in _emailDomainToRole)
            {
                if (email.EndsWith(domain, StringComparison.OrdinalIgnoreCase))
                    return role;
            }
            return enumRole.Guest;
        }

        private static object BuildUserResponse(BaseModel user)
        {
            return new
            {
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                user.PhoneNumber,
                user.DateOfBirth,
                Major = user is Student s ? s.Major : null,
                Year = user is Student st ? st.Year : (int?)null,
                Designation = user is Instructor i ? i.Designation : null,
                Position = user is Admin a ? a.Position : null
            };
        }
        #endregion
    }
}
