using CourseMate.Models.Context;
using CourseMate.Models;
using CourseMate.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace CourseMate.Repository
{

    public interface IAdminRepository
    {
        Task<Admin?> GetAdminById(Guid Id);
        Task<Admin?> GetActiveAdminByEmail(string email);
    }
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;
        public AdminRepository(AppDbContext context) => _context = context;

        public async Task<Admin?> GetAdminById(Guid Id)
        {
            return await _context.Admins.FindAsync(Id);
        }

        public async Task<Admin?> GetActiveAdminByEmail(string email)
        {
            return await _context.Admins
                .Where (admin => admin.IsActive == (int)enumStatus.Active && !admin.IsDeleted)
                .FirstOrDefaultAsync(a => a.Email == email);
        }
    }
}
