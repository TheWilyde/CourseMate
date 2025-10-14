using CourseMate.Models;
using CourseMate.Models.Context;
using CourseMate.Repository;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CourseMate.Services
{
    public interface ICourseRepositoryService
    {
        Task<Course?> GetCourseById(Guid courseId);
    }
    public class CourseRepositoryService : ICourseRepositoryService
    {
        private readonly ICourseRepository _courseRepo;
        public CourseRepositoryService(ICourseRepository courseRepo)
        {
            _courseRepo = courseRepo;
        }
        public async Task<Course?> GetCourseById(Guid courseId)
        {
            var course = await _courseRepo.GetCourseById(courseId);
            if (course is null)
            {
                return null;
            }
            return course;
        }

        public async Task<IEnumerable<Course>> GetCoursesByStudent(Guid studentId)
        {
            var courses = await _courseRepo.GetCoursesByStudent(studentId);
            return courses;
        }
    }
}
