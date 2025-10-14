using CourseMate.Models;
using CourseMate.Models.Context;
using CourseMate.Models.Dtos;
using CourseMate.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace CourseMate.Repository
{
    public interface IStudentRepository
    {
        Task<Student?> GetStudentById(Guid id);
        Task<Student?> GetActiveStudentByEmail(string email);
        Task<IEnumerable<Student>> GetAllStudents();
        Task SignUpStudent(Student student);
        Task UpdateStudent(Student student);
        Task<Student?> UpdateStudentGraduationStatus(Student student, bool isGraduated);
        Task DeleteStudent(Guid id);
    }

    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;
        public StudentRepository(AppDbContext context) => _context = context;

        public async Task<Student?> GetStudentById(Guid id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<Student?> GetActiveStudentByEmail(string email)
        {
            return await _context.Students
                .Where(student => student.IsActive == (int)enumStatus.Active && !student.IsDeleted)
                .FirstOrDefaultAsync(s => s.Email == email);
        }

        public async Task SignUpStudent(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task UpdateStudent(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudent(Guid id)
        {
            var student = await GetStudentById(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Student?> UpdateStudentGraduationStatus(Student student, bool isGraduated)
        {
            student.IsGraduated = isGraduated;
            student.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return student;
        }
    }
}
