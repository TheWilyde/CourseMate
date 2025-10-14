using CourseMate.Models.Context;
using CourseMate.Models;
using Microsoft.EntityFrameworkCore;


namespace CourseMate.Repository
{

    public interface IInstructorRepository
    {
        public Task<Instructor?> GetInstructorById(Guid Id);
        public Task<Instructor?> GetActiveInstructorByEmail(string email);
        public Task<Instructor?> SignUpInstructor(Instructor instructor);
    }

    public class InstructorRepository : IInstructorRepository 
    {
        private readonly AppDbContext _context;
        public InstructorRepository(AppDbContext context) => _context = context;
        
        public async Task<Instructor?> GetInstructorById(Guid Id)
        {
            return await _context.Instructors.FindAsync(Id);
        }

        public async Task<Instructor?> GetActiveInstructorByEmail(string email)
        {
            return await _context.Instructors
                .Where(instructor => instructor.IsActive == 1 && !instructor.IsDeleted)
                .FirstOrDefaultAsync(i => i.Email == email);
        }

        public async Task<Instructor?> SignUpInstructor(Instructor instructor)
        {
            await _context.Instructors.AddAsync(instructor);
            await _context.SaveChangesAsync();
            return instructor;
        }
    }
}
