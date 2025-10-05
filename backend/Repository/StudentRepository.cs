using CourseMate.Models;
using CourseMate.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace CourseMate.Repository
{
    public interface IStudentRepository
    {
        Task<Student?> GetStudentById(Guid id);
        Task<IEnumerable<Student>> GetAllStudents();
        Task AddStudent(Student student);
        Task UpdateStudent(Student student);
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

        public async Task AddStudent(Student student)
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
    }
}
