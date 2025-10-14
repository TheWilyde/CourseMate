using CourseMate.Models;
using CourseMate.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace CourseMate.Repository
{
    public interface ICourseRepository
    {
        Task <Course?> GetCourseById(Guid courseId);
        Task<IEnumerable<Course>> GetCoursesByStudent(Guid studentId);

    }
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _context;
        public CourseRepository(AppDbContext context) => _context = context;

        public async Task<Course?> GetCourseById(Guid courseId)
        {
            return await _context.Courses.FindAsync(courseId);
        }
        public async Task<IEnumerable<Course>> GetCoursesByStudent(Guid studentId)
        {
            var currentSemester = await _context.Semesters
                .Where(s => s.StartDate <= DateTime.UtcNow && s.EndDate >= DateTime.UtcNow)
                .FirstOrDefaultAsync();

            if (currentSemester == null)
                return [];

            return await _context.Enrollments
                .Include(e => e.CourseOffering)
                    .ThenInclude(co => co.Course)
                .Where(e => e.StudentId == studentId && e.CourseOffering.SemesterId == currentSemester.SemesterId)
                .Select(e => e.CourseOffering.Course)
                .ToListAsync();
        }

    }
}
