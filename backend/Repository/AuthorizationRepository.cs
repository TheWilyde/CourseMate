using CourseMate.Models;
using CourseMate.Services;

namespace CourseMate.Repository
{
    public interface IAuthorizationRepository
    {

    }
    public class AuthorizationRepository : IAuthorizationRepository
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IInstructorRepository _instructorRepo;
        private readonly IAdminRepository _adminRepo;

        public AuthorizationRepository(IStudentRepository studentRepo, IInstructorRepository instructorRepo, IAdminRepository adminRepo)
        {
            _studentRepo = studentRepo;
            _instructorRepo = instructorRepo;
            _adminRepo = adminRepo;
        }
    }
}
